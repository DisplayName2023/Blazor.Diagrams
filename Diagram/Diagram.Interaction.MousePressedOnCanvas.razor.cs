﻿using Microsoft.AspNetCore.Components.Web;
using System;

namespace Excubo.Blazor.Diagrams
{
    public partial class Diagram
    {
        private sealed class MousePressedOnCanvas : InteractionState
        {
            public MousePressedOnCanvas(InteractionState previous) : base(previous) { }
            public override InteractionState OnMouseMove(MouseEventArgs e)
            {
                return new Panning(this, new Point(e.ClientX, e.ClientY));
            }
            public override InteractionState OnMouseUp(MouseEventArgs e)
            {
                ResetSelection();
                return new Default(this);
            }
            public override InteractionState OnMouseDown(MouseEventArgs e)
            {
                throw new InvalidOperationException($"The mouse cannot be pressed while the state is {nameof(MousePressedOnCanvas)}");
            }
            private void ResetSelection()
            {
                foreach (var node in diagram.Group.Nodes)
                {
                    node.Deselect();
                }
                diagram.Group.Clear();
            }
        }
    }
}