﻿using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace Excubo.Blazor.Diagrams
{
    public partial class Diagram
    {
        internal Changes Changes = new Changes();
    }
    public partial class Diagram
    {
        private void OnKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Escape")
            {
                foreach (var node in Group.Nodes)
                {
                    node.Deselect();
                }
                Group.Clear();
            }
            else if (e.Key == "z" && e.CtrlKey && !e.ShiftKey)
            {
                Changes.Undo();
                Overview?.TriggerUpdate();
            }
            else if ((e.Key == "Z" && e.CtrlKey && e.ShiftKey)
                  || (e.Key == "y" && e.CtrlKey && !e.ShiftKey))
            {
                Changes.Redo();
                Overview?.TriggerUpdate();
            }
            else if (e.Key == "Delete" || e.Key == "Backspace")
            {
                if (ActionObject.Link != null)
                {
                    var link = ActionObject.Link;
                    Changes.NewAndDo(new ChangeAction(() => Links.Remove(link), () => Links.Add(link)));
                }
                else if (Group.Nodes.Any())
                {
                    var nodes = Group.Nodes.ToList();
                    Changes.NewAndDo(new ChangeAction(() =>
                    {
                        foreach (var node in nodes)
                        {
                            Nodes.Remove(node);
                        }
                    }, () =>
                    {
                        foreach (var node in nodes)
                        {
                            Nodes.Add(node);
                        }
                    }));
                    OnRemove?.Invoke(Group);
                    Group.Clear();
                }
                Overview?.TriggerUpdate();
            }
        }
    }
}