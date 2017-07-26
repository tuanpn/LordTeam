using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class TaskBar : MonoBehaviour
    {
        public GameObject rankLabel;
        public GameObject goldLabel;
        public GameObject timeLabel;
        public GameObject scoreLabel;
        public Dots dots;

        private BitmapFont rankFont;
        private BitmapFont fontGold;
        private BitmapFont fontTime;
        private BitmapFont fontScore;

        private int gold;
        private int score;
        private float time;

        public void Start()
        {
            rankFont = new BitmapFont("Fonts/font_rank", "Fonts/font_rank_xml", rankLabel);
            
            fontTime = new BitmapFont("Fonts/font_time", "Fonts/font_time_xml", timeLabel);
            fontGold = new BitmapFont(fontTime, goldLabel);
            fontScore = new BitmapFont(fontTime, scoreLabel);
        }

        public void Update()
        {
            rankFont.setText(dots.getRankPlayer() + "", 0, 0, "GUI", "GUI");
            fontTime.setText(((int)(time * 100)) / 100.0f + "", 0, 0, "GUI", "GUI");
            fontGold.setText(gold + "", 0, 0, "GUI", "GUI");
            fontScore.setText(score + "", 0, 0, "GUI", "GUI");
        }

        public int getRankPlayer()
        {
            return dots.getRankPlayer();
        }

        public void setParams(int gold, int score, float time)
        {
            this.gold = gold;
            this.score = score;
            this.time = time;
        }
    }
}