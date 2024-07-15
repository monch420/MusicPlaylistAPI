using System.Collections.Generic;
using Model;

namespace DataLayer
{
    public class PlaylistDataLayer
    {
        SqlDbData dbData = new SqlDbData();

        public List<Song> ReturnPlaylistInformation()
        {
            return dbData.GetSongs();
        }

        public Song GetSong(string songTitle)
        {
            return dbData.GetSong(songTitle);
        }

        public List<Song> DeleteSong(string songTitle)
        {
            dbData.DeleteSong(songTitle);
            return dbData.GetSongs();
        }

        public List<Song> AddSong(string userTitle, string userArtist, string userAlbum, string userDuration)
        {
            Song newSong = new Song
            {
                title = userTitle,
                artist = userArtist,
                album = userAlbum,
                duration = userDuration
            };

            dbData.AddSong(newSong);
            return dbData.GetSongs();
        }
    }
}