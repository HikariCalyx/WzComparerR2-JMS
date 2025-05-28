﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WzComparerR2.Rendering
{
    public class PngEffect : Effect
    {
        public PngEffect(GraphicsDevice graphicDevice)
            :base(graphicDevice, GetEffectCode())
        {
            this.AlphaMixEnabled = false;
            this.MinMixedAlpha = 255;
            this.MixedColor = Color.White;
            this.Overlay = false;
        }

        public bool AlphaMixEnabled
        {
            get { return alphaMixed; }
            set
            {
                this.CurrentTechnique = this.Techniques[value ? "tech1" : "tech0"];
                this.alphaMixed = value;
            }
        }

        public int MinMixedAlpha
        {
            get { return (int)(this.Parameters["clipAlpha"].GetValueSingle() * 255); }
            set { this.Parameters["clipAlpha"].SetValue((float)value / 255); }
        }

        public Color MixedColor
        {
            get { return new Color(this.Parameters["mixedColor"].GetValueVector4()); }
            set { this.Parameters["mixedColor"].SetValue(value.ToVector4()); }
        }

        public bool Overlay
        {
            get { return overlay_; }
            set
            {
                if (value)
                {
                    this.CurrentTechnique = this.Techniques["techov"];
                }
                this.overlay_ = value;
            }
        }

        private bool alphaMixed;
        private bool overlay_;

        private static byte[] GetEffectCode()
        {
            var asm = Assembly.GetAssembly(typeof(PngEffect));
            
            using (var input = asm.GetManifestResourceStream("WzComparerR2.Rendering.Effect.PngEffect.mgfxo"))
            {
                byte[] code = new byte[input.Length];
                input.Read(code, 0, code.Length);
                return code;
            }
        }
    }
}
