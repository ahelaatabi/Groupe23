﻿//-----------------------------------------------------------------------
// Copyright 2015 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

namespace PannableForms
{
    using System.Windows.Forms;
    using EyeXFramework;
    using EyeXFramework.Forms;
    using Tobii.EyeX.Framework;

    public partial class PannableForm : Form
    {
        public PannableForm()
        {
            InitializeComponent();

            // Make the text box on the form EyeX scrollable using the Pannable behavior.
            // When the user looks in the lower/upper part of the pannable area and presses and
            // holds the EyeX Button on the keyboard, the the text in the window will scroll
            // up/down.
            // The panning profile decides the velocities to trigger at different parts of the 
            // pannable area, like for example where to switch from one direction to another.
            // The vertical panning profile uses velocities optimized for vertical scrolling.
            // In this sample we are using a simplified panning implementation that only uses 
            // the direction and not the magnitude of the velocity.
            // Still, you can try and change the profile to PanningProfile.Radial and compare 
            // the panning behavior of where it changes direction.
            Program.Host.Connect(_behaviorMap);
            _behaviorMap.Add(_text, new PannableBehavior(OnPanning) { PanDirectionsAvailable = PanDirection.Down | PanDirection.Up, Profile = PanningProfile.Vertical });
        }

        private void OnPanning(object sender, PannablePanEventArgs e)
        {
            if (e.PanVelocityY != 0D)
            {
                // Perform scrolling.
                // To simplify the implementation of this code sample, the magnitude of the 
                // velocity is not used - we are simply switching the scrolling on and off in 
                // the right direction. 
                // Please note that to scroll down the text should move up, and vice versa.
                _text.Scroll(e.PanVelocityY > 0 ? -1 : 1); 
            }
        }
    }
}
