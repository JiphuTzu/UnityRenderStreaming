﻿using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.WebRTC
{
    public class MediaStreamTrack
    {
        internal IntPtr nativePtr;
        protected string id;
        protected TrackKind kind;

        internal MediaStreamTrack(IntPtr ptr)
        {
            nativePtr = ptr;
            kind = NativeMethods.MediaStreamTrackGetKind(nativePtr);
            id = Marshal.PtrToStringAnsi(NativeMethods.MediaStreamTrackGetID(nativePtr));
        }

        public bool Enabled
        {
            get { return NativeMethods.MediaStreamTrackGetEnabled(nativePtr); }
            set { NativeMethods.MediaStreamTrackSetEnabled(nativePtr, value); }
        }

        public TrackState ReadyState
        {
            get
            { return NativeMethods.MediaStreamTrackGetReadyState(nativePtr); }
            private set { }
        }

        public TrackKind Kind { get => kind; private set { } }
        public string Id { get => id; private set { } }
    }

    public class VideoStreamTrack : MediaStreamTrack
    {
        public VideoStreamTrack(string label, RenderTexture rt, int bitRateMbps=10000000) : base(WebRTC.Context.CreateVideoTrack(label, rt.GetNativeTexturePtr(), rt.width, rt.height, bitRateMbps))
        {
        }
    }

    public class AudioStreamTrack : MediaStreamTrack
    {
        public AudioStreamTrack(string label) : base(WebRTC.Context.CreateAudioTrack(label))
        {
        }
    }


    public enum TrackKind
    {
        Audio,
        Video
    }
    public enum TrackState
    {
        Live,
        Ended
    }
    public class RTCRtpSender
    {
        internal IntPtr self;
        private MediaStreamTrack track;

        internal RTCRtpSender(IntPtr ptr)
        {
            self = ptr;
        }
    }
    public class RTCTrackEvent
    {
        private IntPtr self;
        private MediaStreamTrack track;

        public MediaStreamTrack Track
        {
            get => new MediaStreamTrack(NativeMethods.RtpTransceiverInterfaceGetTrack(self));
            private set { }
        }
        internal RTCTrackEvent(IntPtr ptr)
        {
            self = ptr;
        }
    }

}

