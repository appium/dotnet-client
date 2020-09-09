using System;
using System.Collections.Generic;
using System.Text;

namespace OpenQA.Selenium.Appium.Android
{
    public class KeyEvent
    {
        private int? _keyCode; 
        private int? _metaState;
        private int? _flags;

        public KeyEvent WithKeyCode(int? androidKeyCode)
        {
            _keyCode = androidKeyCode;
            return this;
        }

        public KeyEvent WithMetaState(int? metaState)
        {
            _metaState = metaState;
            return this;
        }

        public KeyEvent WithFlag(int? flag)
        {
            _flags = flag;
            return this;
        }

        public Dictionary<string, object> Build()
        {
            var builder = new Dictionary<string, object>();
            if (_keyCode is null) throw new InvalidOperationException("The key code must be set");
            builder.Add("keycode", _keyCode);
            if (!(_metaState is null)) builder.Add("metastate", _metaState);
            if (!(_flags is null)) builder.Add("flags", _flags);

            return builder;
        }

    }
}
