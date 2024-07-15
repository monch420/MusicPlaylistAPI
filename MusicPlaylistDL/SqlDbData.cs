using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataLayer
{
    public class SqlDbData
    {
        private string connectionString = "Data Source =DESKTOP-3636T04\\SQLEXPRESS; Initial Catalog = MusicPlaylist; Integrated Security = True;";

        public List<Song> GetSongs()
        {
            List<Song> songs = new List<Song>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT title, artist, album, duration FROM songs", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Song song = new Song
                        {
                            title = reader["title"].ToString(),
                            artist = reader["artist"].ToString(),
                            album = reader["album"].ToString(),
                            duration = reader["duration"].ToString()
                        };

                        songs.Add(song);
                    }
                }
            }

            return songs;
        }

        public Song GetSong(string songTitle)
        {
            Song song = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT title, artist, album, duration FROM songs WHERE title = @title", conn);
                cmd.Parameters.AddWithValue("@title", songTitle);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        song = new Song
                        {
                            title = reader["title"].ToString(),
                            artist = reader["artist"].ToString(),
                            album = reader["album"].ToString(),
                            duration = reader["duration"].ToString()
                        };
                    }
                }
            }

            return song;
        }

        public void DeleteSong(string songTitle)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM songs WHERE title = @title", conn);
                cmd.Parameters.AddWithValue("@title", songTitle);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSong(Song newSong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO songs (title, artist, album, duration) VALUES (@title, @artist, @album, @duration)", conn);
                cmd.Parameters.AddWithValue("@title", newSong.title);
                cmd.Parameters.AddWithValue("@artist", newSong.artist);
                cmd.Parameters.AddWithValue("@album", newSong.album);
                cmd.Parameters.AddWithValue("@duration", newSong.duration);
                cmd.ExecuteNonQuery();
            }
        }
    }
}