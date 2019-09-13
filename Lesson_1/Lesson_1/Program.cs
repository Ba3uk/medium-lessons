using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2_3
{
    public class ConsoleCursorController {
        private static ConsoleCursorController _instance { set; get; }
        public static ConsoleCursorController Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConsoleCursorController();
                }
                return _instance;
            }
        }

        public delegate void ConsoleDelegate(Vector2 vector);
        public event ConsoleDelegate OnClick;
        public event ConsoleDelegate OnChangePosition;

        public Vector2 CoursorPosition { get; private set; }
        public void MoveCursor()
        {
            // бла бла 
            // движение курсора
            OnChangePosition?.Invoke(CoursorPosition);
        }
        public void Click()
        {
            // бла бла 
            // что будет при клике
            OnClick?.Invoke(CoursorPosition);
        }

    }

    public abstract class UIObject
    {
        public Frame frame;
        public int Hight;
        public int Withg;
        public Vector2 Anchor;

        public UIObject()
        {
            ConsoleCursorController.Instance.OnClick += onClick;
            ConsoleCursorController.Instance.OnChangePosition += onSubmit;
        }


        private void onSubmit(Vector2 coursorPosition)
        {
            if (isCollisionWithCoursor(coursorPosition))
            {
                frame.ChangeColor(Green);
            }
        }
        public abstract void Action();
        private void onClick(Vector2 coursorPosition)
        {
            if (isCollisionWithCoursor(coursorPosition))
            {
                Action();
            }
        }
        private bool isCollisionWithCoursor(Vector2 coursorPosition)
        {
            if (coursorPosition.X < Anchor.X + Withg ||coursorPosition.X > Anchor.X &&
                coursorPosition.Y < Anchor.Y + Hight ||coursorPosition.Y > Anchor.Y)
            {
                return true;
            }
            return false;
        }

    }

    public class Vector2
    {
        public int X;
        public int Y;
    }


}
