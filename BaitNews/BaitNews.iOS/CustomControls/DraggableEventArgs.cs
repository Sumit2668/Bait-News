using System;

//Credit to Jon Davis
namespace BaitNews.iOS
{
    public class DraggableEventArgs : EventArgs
    {
        public DraggableDirection Dragged { get; private set; }

        public DraggableEventArgs(DraggableDirection dragged)
        {
            Dragged = dragged;
        }
    }
}

