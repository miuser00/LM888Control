using System;
using System.IO;
using Un4seen.Bass;

namespace _3D8_2012
{
    internal class BassPlayer
    {
        private int _stream;

        private string _filepath = string.Empty;

        private int _deviceLatency;

        private int _deviceLatencyInBytes;

        private int _10mslength;

        private long _lengthInByte;

        private long _positionInByte;

        public static readonly int[] c_Index = new int[]
        {
            1,
            2,
            3,
            5,
            8,
            12,
            18,
            27
        };

        private static int[] pFFT;

        public void SetFilePath(string s)
        {
            this._filepath = s;
        }

        public string GetFileName()
        {
            return System.IO.Path.GetFileName(this._filepath);
        }

        public string GetTimeLineString()
        {
            this._positionInByte = Bass.BASS_ChannelGetPosition(this._stream);
            double seconds = Bass.BASS_ChannelBytes2Seconds(this._stream, this._positionInByte);
            double seconds2 = Bass.BASS_ChannelBytes2Seconds(this._stream, this._lengthInByte);
            return string.Format(" {0:#0.00} / {1:#0.00}", Utils.FixTimespan(seconds, "MMSS"), Utils.FixTimespan(seconds2, "MMSS"));
        }

        public void Stop()
        {
            this._positionInByte = 0L;
            Bass.BASS_ChannelSetPosition(this._stream, this._positionInByte);
            Bass.BASS_StreamFree(this._stream);
            this._stream = 0;
        }

        public void Pause()
        {
            if (Bass.BASS_ChannelIsActive(this._stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                Bass.BASS_ChannelPause(this._stream);
                return;
            }
            Bass.BASS_ChannelPlay(this._stream, false);
        }

        public BassPlayer(System.IntPtr win)
        {
            BassPlayer.pFFT = new int[BassPlayer.c_Index.Length];
            BassNet.Registration("weihong.guan@gmail.com", "2X32231422152222");
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_LATENCY, win))
            {
                BASS_INFO bASS_INFO = new BASS_INFO();
                Bass.BASS_GetInfo(bASS_INFO);
                this._deviceLatency = bASS_INFO.latency;
                return;
            }
            System.Console.WriteLine("Bass_Init error!");
        }

        public string GetState()
        {
            string result = string.Empty;
            switch (Bass.BASS_ChannelIsActive(this._stream))
            {
                case BASSActive.BASS_ACTIVE_STOPPED:
                    result = "Stopped";
                    break;
                case BASSActive.BASS_ACTIVE_PLAYING:
                    result = "Playing";
                    break;
                case BASSActive.BASS_ACTIVE_PAUSED:
                    result = "Paused";
                    break;
            }
            return result;
        }

        public int GetPositionInSecond()
        {
            return (int)Bass.BASS_ChannelBytes2Seconds(this._stream, this._positionInByte);
        }

        public int GetLengthInSecond()
        {
            return (int)Bass.BASS_ChannelBytes2Seconds(this._stream, this._lengthInByte);
        }

        public void SetPosition(int iPositionInSecond)
        {
            double pos = System.Convert.ToDouble(iPositionInSecond);
            this._positionInByte = Bass.BASS_ChannelSeconds2Bytes(this._stream, pos);
            Bass.BASS_ChannelSetPosition(this._stream, this._positionInByte);
        }

        public bool Play()
        {
            bool result = false;
            Bass.BASS_StreamFree(this._stream);
            if (this._filepath != string.Empty)
            {
                this._stream = Bass.BASS_StreamCreateFile(this._filepath, 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);
                if (this._stream != 0 && Bass.BASS_ChannelPlay(this._stream, false))
                {
                    Bass.BASS_ChannelSetPosition(this._stream, this._positionInByte);
                    this._10mslength = (int)Bass.BASS_ChannelSeconds2Bytes(this._stream, 0.01);
                    this._deviceLatencyInBytes = (int)Bass.BASS_ChannelSeconds2Bytes(this._stream, (double)((float)this._deviceLatency / 1000f));
                    this._lengthInByte = Bass.BASS_ChannelGetLength(this._stream);
                    result = true;
                }
                else
                {
                    System.Console.WriteLine(string.Format("Error={0}", Bass.BASS_ErrorGetCode()));
                }
            }
            return result;
        }

        public int[] GetFFT(float fMag, int iMax)
        {
            float[] array = new float[256];
            Bass.BASS_ChannelGetData(this._stream, array, -2147483647);
            for (int i = 0; i < BassPlayer.c_Index.Length; i++)
            {
                int num = (int)System.Math.Round((double)(array[BassPlayer.c_Index[i]] * fMag));
                if (num > iMax)
                {
                    num = iMax;
                }
                BassPlayer.pFFT[i] = num;
            }
            return BassPlayer.pFFT;
        }

        public void GetRMS(float fMag, int fMax, out int peakL, out int peakR)
        {
            float num = 0f;
            float num2 = 0f;
            float[] array = new float[256];
            Bass.BASS_ChannelGetData(this._stream, array, 1024);
            for (int i = 0; i < array.Length; i++)
            {
                float num3 = System.Math.Abs(array[i]);
                float num4 = System.Math.Abs(array[i + 1]);
                if (num3 > num)
                {
                    num = num3;
                }
                if (num4 > num2)
                {
                    num2 = num4;
                }
                i++;
            }
            peakL = (int)System.Math.Round((double)(fMag * num));
            if (peakL > fMax)
            {
                peakL = fMax;
            }
            peakR = (int)System.Math.Round((double)(fMag * num2));
            if (peakR > fMax)
            {
                peakR = fMax;
            }
        }
    }
}
