using System;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Android.Enums;

namespace OpenQA.Selenium.Appium.Android
{
    public class KeyEvent
    {
        private int? _keyCode;
        private int? _metaState;
        private int? _flags;

        public KeyEvent()
        {
        }

        /// <summary>
        /// Creates a new key event
        /// </summary>
        /// <param name="keyCode">The key code. This is mandatory</param>
        /// <see cref="AndroidKeyCode"/>
        public KeyEvent(int keyCode)
        {
            _keyCode = keyCode;
        }

        /// <summary>
        /// Sets the key code.
        /// This is mandatory.
        /// </summary>
        /// <param name="keyCode">The key code</param>
        /// <see cref="AndroidKeyCode"/>
        /// <returns></returns>
        public KeyEvent WithKeyCode(int keyCode)
        {
            _keyCode = keyCode;
            return this;
        }

        /// <summary>
        /// Adds the meta key modifier
        /// Flags indicating which meta keys are currently pressed.
        /// Multiple meta key modifier flags can be combined into a single key event
        /// </summary>
        /// <param name="keyEventMetaModifier">The meta state</param>
        /// <see cref="AndroidKeyMetastate"/>
        /// <returns></returns>
        public KeyEvent WithMetaKeyModifier(int keyEventMetaModifier)
        {
            if (_metaState is null) _metaState = 0;
            _metaState |= keyEventMetaModifier;
            return this;
        }

        /// <summary>
        /// Adds the flag(s).
        /// Multiple flags can be combined into a single key event
        /// </summary>
        /// <param name="flag">The flag</param>
        /// <see cref="AndroidKeyCode"/>
        /// <returns></returns>
        public KeyEvent WithFlag(int flag)
        {
            if (_flags is null) _flags = 0;
            _flags |= flag;
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