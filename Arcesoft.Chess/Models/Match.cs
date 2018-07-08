using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    public class Match
    {
        /// <summary>
        /// Represents the game (as loaded from the pgn file)
        /// </summary>
        public IGame Game { get; set; }

        /// <summary>
        /// Event name
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// Site at which match occurred
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// The year when the match took place (if available)
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// The month when the match took place (if available)
        /// </summary>
        public int? Month { get; set; }
        /// <summary>
        /// The day when the match took place (if available)
        /// </summary>
        public int? Day { get; set; }
        /// <summary>
        /// Which round this match represents
        /// </summary>
        public string Round { get; set; }
        /// <summary>
        /// White players name
        /// </summary>
        public string WhitePlayerName { get; set; }
        /// <summary>
        /// Black players name
        /// </summary>
        public string BlackPlayerName { get; set; }

        /// <summary>
        /// All game tags (aka headers)
        /// </summary>
        public Dictionary<string, string> Tags { get; set; }
    }
}
