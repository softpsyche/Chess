using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcesoft.Chess.Models
{
    //PGN format for the board
    //    A  B  C  D  E  F  G  H
    //8:| 7|15|23|31|39|47|55|63|
    //7:| 6|14|22|30|38|46|54|62|
    //6:| 5|13|21|29|37|45|53|61|
    //5:| 4|12|20|28|36|44|52|60|
    //4:| 3|11|19|27|35|43|51|59|
    //3:| 2|10|18|26|34|42|50|58|
    //2:| 1| 9|17|25|33|41|49|57|
    //1:| 0| 8|16|24|32|40|48|56|
    internal static class BoardLocationExtensions
    {
        

        public static bool IsPawnStartingLocation(this BoardLocation boardLocation, Player player)
        {
            if (player == Player.White)
            {
                return boardLocation.ToByte() % 8 == 1;
            }
            else
            {
                return boardLocation.ToByte() % 8 == 6;
            }
        }

        public static bool HasThreats(this ThreatDirection? threatDirection, ThreatDirection threat)
        {
            if (threatDirection.HasValue)
            {
                return (threatDirection.Value & threat) > 0;
            }

            return false;
        }

        public static bool HasMultipleThreats(this ThreatDirection? threatDirection)
        {
            if (threatDirection.HasValue)
            {
                return ((ThreatDirection[])Enum.GetValues(typeof(ThreatDirection)))
                    .Count(a => threatDirection.Value.HasFlag(a)) > 1;
            }

            return false;
        }

        public static IEnumerable<BoardLocation> Neighbors(this BoardLocation? currentLocation)
            => currentLocation.HasValue ? currentLocation.Value.Neighbors() : Enumerable.Empty<BoardLocation>();

        public static IEnumerable<BoardLocation> Neighbors(this BoardLocation currentLocation)
        {
            List<BoardLocation> boardLocations = new List<BoardLocation>();
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                var result = currentLocation.Neighbor(dir);

                if (result.HasValue)
                {
                    boardLocations.Add(result.Value);
                }
            }

            return boardLocations;
        }

        public static BoardLocation? Neighbor(this BoardLocation? currentLocation, Direction direction) => currentLocation?.Neighbor(direction);

        public static BoardLocation? Neighbor(this BoardLocation currentLocation, Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return currentLocation.East();
                case Direction.North:
                    return currentLocation.North();
                case Direction.NorthEast:
                    return currentLocation.NorthEast();
                case Direction.NorthWest:
                    return currentLocation.NorthWest();
                case Direction.South:
                    return currentLocation.South();
                case Direction.SouthEast:
                    return currentLocation.SouthEast();
                case Direction.SouthWest:
                    return currentLocation.SouthWest();
                case Direction.West:
                    return currentLocation.West();
                default:
                    return null;
            }
        }

        public static BoardLocation? NorthWest(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() - 7;

            //if we DONT hop columns we are out of bounds
            if (val / 8 == currentLocation.Column()) return null;

            return (val >= 0) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static BoardLocation? NorthEast(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() + 9;

            //if we are not the next column this is invalid
            if ((val / 8) - 1 != currentLocation.Column()) return null;

            return (val < Constants.BoardSize) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static BoardLocation? SouthWest(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() - 9;

            //if we are not the next column this is invalid
            if ((val / 8) + 1 != currentLocation.Column()) return null;

            return (val >= 0) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static BoardLocation? SouthEast(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() + 7;

            //if we are not the next column this is invalid
            if ((val / 8) == currentLocation.Column()) return null;

            return (val < Constants.BoardSize) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static BoardLocation? North(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() + 1;

            //if we hop columns we are out of bounds
            if (val / 8 != currentLocation.Column()) return null;

            return (val < Constants.BoardSize) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static BoardLocation? South(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() - 1;

            //if we hop columns we are out of bounds
            if (val / 8 != currentLocation.Column()) return null;

            return (val >= 0) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static BoardLocation? East(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() + Constants.BoardWidth;

            return (val < Constants.BoardSize) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static BoardLocation? West(this BoardLocation currentLocation)
        {
            var val = currentLocation.ToByte() - Constants.BoardWidth;

            return (val >= 0) ? (BoardLocation)val : default(BoardLocation?);
        }

        public static bool IsInGivenDirectionFrom(this BoardLocation source, BoardLocation target, Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return source.IsEastFrom(target);
                case Direction.North:
                    return source.IsNorthFrom(target);
                case Direction.NorthEast:
                    return source.IsNorthEastFrom(target);
                case Direction.NorthWest:
                    return source.IsNorthWestFrom(target);
                case Direction.South:
                    return source.IsSouthFrom(target);
                case Direction.SouthEast:
                    return source.IsSouthEastFrom(target);
                case Direction.SouthWest:
                    return source.IsSouthWestFrom(target);
                case Direction.West:
                    return source.IsWestFrom(target);
                default:
                    return false;
            }
        }

        public static bool IsWestFrom(this BoardLocation source, BoardLocation target)
        {
            return ((source.ToByte() < target.ToByte()) &&
                    source.IsOnSameRowAs(target));
        }
        public static bool IsEastFrom(this BoardLocation source, BoardLocation target)
        {
            return ((source.ToByte() > target.ToByte()) &&
                    source.IsOnSameRowAs(target));
        }

        public static bool IsNorthFrom(this BoardLocation source, BoardLocation target)
        {
            return ((source.ToByte() > target.ToByte()) &&
                    source.IsOnSameColumnAs(target));
        }
        public static bool IsSouthFrom(this BoardLocation source, BoardLocation target)
        {
            return ((source.ToByte() > target.ToByte()) &&
                    source.IsOnSameColumnAs(target));
        }

        public static bool IsNorthWestFrom(this BoardLocation source, BoardLocation target)
        {
            //PGN format for the board
            //    A  B  C  D  E  F  G  H
            //8:| 7|15|23|31|39|47|55|63|
            //7:| 6|14|22|30|38|46|54|62|
            //6:| 5|13|21|29|37|45|53|61|
            //5:| 4|12|20|28|36|44|52|60|
            //4:| 3|11|19|27|35|43|51|59|
            //3:| 2|10|18|26|34|42|50|58|
            //2:| 1| 9|17|25|33|41|49|57|
            //1:| 0| 8|16|24|32|40|48|56|
            return ((source.ToByte() < target.ToByte()) && 
                    source.IsOnSameSlopeAs(target));
        }
        public static bool IsSouthWestFrom(this BoardLocation source, BoardLocation target)
        {
            return ((source.ToByte() > target.ToByte()) &&
                    source.IsOnSameGradeAs(target));
        }

        public static bool IsNorthEastFrom(this BoardLocation source, BoardLocation target)
        {
            return ((source.ToByte() > target.ToByte()) &&
                    source.IsOnSameGradeAs(target));
        }
        public static bool IsSouthEastFrom(this BoardLocation source, BoardLocation target)
        {
            return ((source.ToByte() < target.ToByte()) &&
                    source.IsOnSameSlopeAs(target));
        }

        public static bool IsOnSameSlopeAs(this BoardLocation source, BoardLocation target)
        {
            return source.Slope() == target.Slope();
        }

        public static bool IsOnSameGradeAs(this BoardLocation source, BoardLocation target)
        {
            return source.Grade() == target.Grade();
        }

        public static bool IsOnSameColumnAs(this BoardLocation source, BoardLocation target)
        {
            return source.Column() == target.Column();
        }

        public static bool IsOnSameRowAs(this BoardLocation source, BoardLocation target)
        {
            return source.Row() == target.Row();
        }

        public static int Slope(this BoardLocation boardLocation)
        {
            //PGN format for the board
            //    A  B  C  D  E  F  G  H
            //8:| 7|15|23|31|39|47|55|63|
            //7:| 6|14|22|30|38|46|54|62|
            //6:| 5|13|21|29|37|45|53|61|
            //5:| 4|12|20|28|36|44|52|60|
            //4:| 3|11|19|27|35|43|51|59|
            //3:| 2|10|18|26|34|42|50|58|
            //2:| 1| 9|17|25|33|41|49|57|
            //1:| 0| 8|16|24|32|40|48|56|
            switch (boardLocation)
            {
                case BoardLocation.A1:
                    return 0;
                case BoardLocation.A2:
                case BoardLocation.B1:
                    return 1;
                case BoardLocation.A3:
                case BoardLocation.B2:
                case BoardLocation.C1:
                    return 2;
                case BoardLocation.A4:
                case BoardLocation.B3:
                case BoardLocation.C2:
                case BoardLocation.D1:
                    return 3;
                case BoardLocation.A5:
                case BoardLocation.B4:
                case BoardLocation.C3:
                case BoardLocation.D2:
                case BoardLocation.E1:
                    return 4;
                case BoardLocation.A6:
                case BoardLocation.B5:
                case BoardLocation.C4:
                case BoardLocation.D3:
                case BoardLocation.E2:
                case BoardLocation.F1:
                    return 5;
                case BoardLocation.A7:
                case BoardLocation.B6:
                case BoardLocation.C5:
                case BoardLocation.D4:
                case BoardLocation.E3:
                case BoardLocation.F2:
                case BoardLocation.G1:
                    return 6;
                case BoardLocation.A8:
                case BoardLocation.B7:
                case BoardLocation.C6:
                case BoardLocation.D5:
                case BoardLocation.E4:
                case BoardLocation.F3:
                case BoardLocation.G2:
                case BoardLocation.H1:
                    return 7;
                case BoardLocation.B8:
                case BoardLocation.C7:
                case BoardLocation.D6:
                case BoardLocation.E5:
                case BoardLocation.F4:
                case BoardLocation.G3:
                case BoardLocation.H2:
                    return 8;
                case BoardLocation.C8:
                case BoardLocation.D7:
                case BoardLocation.E6:
                case BoardLocation.F5:
                case BoardLocation.G4:
                case BoardLocation.H3:
                    return 9;
                case BoardLocation.D8:
                case BoardLocation.E7:
                case BoardLocation.F6:
                case BoardLocation.G5:
                case BoardLocation.H4:
                    return 10;
                case BoardLocation.E8:
                case BoardLocation.F7:
                case BoardLocation.G6:
                case BoardLocation.H5:
                    return 11;
                case BoardLocation.F8:
                case BoardLocation.G7:
                case BoardLocation.H6:
                    return 12;
                case BoardLocation.G8:
                case BoardLocation.H7:
                    return 13;
                case BoardLocation.H8:
                    return 14;
                default:
                    throw new ArgumentException();
            }
        }

        public static int Grade(this BoardLocation boardLocation)
        {
            //PGN format for the board
            //    A  B  C  D  E  F  G  H
            //8:| 7|15|23|31|39|47|55|63|
            //7:| 6|14|22|30|38|46|54|62|
            //6:| 5|13|21|29|37|45|53|61|
            //5:| 4|12|20|28|36|44|52|60|
            //4:| 3|11|19|27|35|43|51|59|
            //3:| 2|10|18|26|34|42|50|58|
            //2:| 1| 9|17|25|33|41|49|57|
            //1:| 0| 8|16|24|32|40|48|56|

            switch (boardLocation)
            {
                case BoardLocation.A8:
                    return 0;
                case BoardLocation.A7:
                case BoardLocation.B8:
                    return 1;
                case BoardLocation.A6:
                case BoardLocation.B7:
                case BoardLocation.C8:
                    return 2;
                case BoardLocation.A5:
                case BoardLocation.B6:
                case BoardLocation.C7:
                case BoardLocation.D8:
                    return 3;
                case BoardLocation.A4:
                case BoardLocation.B5:
                case BoardLocation.C6:
                case BoardLocation.D7:
                case BoardLocation.E8:
                    return 4;
                case BoardLocation.A3:
                case BoardLocation.B4:
                case BoardLocation.C5:
                case BoardLocation.D6:
                case BoardLocation.E7:
                case BoardLocation.F8:
                    return 5;
                case BoardLocation.A2:
                case BoardLocation.B3:
                case BoardLocation.C4:
                case BoardLocation.D5:
                case BoardLocation.E6:
                case BoardLocation.F7:
                case BoardLocation.G8:
                    return 6;
                case BoardLocation.A1:
                case BoardLocation.B2:
                case BoardLocation.C3:
                case BoardLocation.D4:
                case BoardLocation.E5:
                case BoardLocation.F6:
                case BoardLocation.G7:
                case BoardLocation.H8:
                    return 7;
                case BoardLocation.B1:
                case BoardLocation.C2:
                case BoardLocation.D3:
                case BoardLocation.E4:
                case BoardLocation.F5:
                case BoardLocation.G6:
                case BoardLocation.H7:
                    return 8;
                case BoardLocation.C1:
                case BoardLocation.D2:
                case BoardLocation.E3:
                case BoardLocation.F4:
                case BoardLocation.G5:
                case BoardLocation.H6:
                    return 9;
                case BoardLocation.D1:
                case BoardLocation.E2:
                case BoardLocation.F3:
                case BoardLocation.G4:
                case BoardLocation.H5:
                    return 10;
                case BoardLocation.E1:
                case BoardLocation.F2:
                case BoardLocation.G3:
                case BoardLocation.H4:
                    return 11;
                case BoardLocation.F1:
                case BoardLocation.G2:
                case BoardLocation.H3:
                    return 12;
                case BoardLocation.G1:
                case BoardLocation.H2:
                    return 13;
                case BoardLocation.H1:
                    return 14;
                default:
                    throw new ArgumentException();
            }
        }

        public static int Row(this BoardLocation boardLocation) => (byte)boardLocation % 8;

        public static int Column(this BoardLocation boardLocation) => (byte)boardLocation / 8;

        public static byte ToByte(this BoardLocation boardLocation) => (byte)boardLocation;
    }

}
