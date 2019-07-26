using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LateralMenu.Control;
using LateralMenu.Model;

namespace LateralMenu.Service
{
    public static class ElementPropagator
    {
        public static void Propagate(IMaterialContainer parent, object elements)
        {
            if (!(elements is IList casted)) return;
            foreach (var expander in casted.OfType<ExpanderButton>())
            {
                expander.SetParent(parent);
                Propagate(expander, expander.Header as IMaterialItem);

                expander.Close();
                Propagate(expander, expander.Children);
            }

            casted.OfType<IMaterialItem>().ToList().ForEach(x => Propagate(parent, x));
        }

        private static void Propagate(IMaterialContainer parent, IMaterialItem item)
        {
            item?.SetParent(parent);
        }
    }
}
