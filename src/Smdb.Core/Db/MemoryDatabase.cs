namespace Smdb.Core.Db;

using Smdb.Core.Movies;
using Smdb.Core.Actors;
using Smdb.Core.Users;
using Smdb.Core.ActorsMovies;
public class MemoryDatabase
{
    public List<Movie> Movies { get; }
    public List<Actor> Actors { get; }
    public List<User> Users { get; }
    public List<ActorMovie> ActorsMovies { get; }
    private int nextMovieId;
    private int nextActorId;
    private int nextUserId;
    public MemoryDatabase()
    {
        Movies = [];
        Actors = [];
        Users = [];
        ActorsMovies = [];


        SeedMovies();
        SeedActors();
        SeedUsers();
        SeedActorsMovies();

        nextMovieId = Movies.Count;
        nextActorId = Actors.Count;
        nextUserId = Users.Count;
    }
    private void SeedMovies()
    {
        Movies.AddRange(new Movie[]
        {
new Movie(1, "The Godfather", 1972, "A mafia patriarch hands the family empire to his reluctant son."),
new Movie(2, "The Godfather Part II", 1974, "Michael consolidates power as flashbacks trace Vito Corleone’s rise."),
new Movie(3, "The Dark Knight", 2008, "Batman faces the Joker, who pushes Gotham into chaos."),
new Movie(4, "The Shawshank Redemption", 1994, "An innocent banker forms a life-saving friendship in prison."),
new Movie(5, "Pulp Fiction", 1994, "Interlocking LA crime stories unfold with dark humor."),
new Movie(6, "Schindler's List", 1993, "A businessman saves Jewish workers during the Holocaust."),
new Movie(7, "The Lord of the Rings: The Return of the King", 2003, "The final push to destroy the One Ring decides Middle-earth’s fate."),
new Movie(8, "Fight Club", 1999, "An insomnia-plagued worker joins a charismatic anarchist’s secret club."),
new Movie(9, "Forrest Gump", 1994, "A kind man unwittingly drifts through historic American moments."),
new Movie(10, "Inception", 2010, "A thief enters dreams to plant an idea in a target’s mind."),
new Movie(11, "The Matrix", 1999, "A hacker learns reality is a simulated prison for humanity."),
new Movie(12, "Se7en", 1995, "Two detectives hunt a killer using the seven deadly sins."),
new Movie(13, "Goodfellas", 1990, "Henry Hill’s rise and fall inside the New York mob."),
new Movie(14, "The Silence of the Lambs", 1991, "An FBI trainee consults Hannibal Lecter to catch a serial killer."),
new Movie(15, "Star Wars: Episode IV – A New Hope", 1977, "A farm boy joins rebels to destroy the Empire’s Death Star."),
new Movie(16, "The Empire Strikes Back", 1980, "The Rebels scatter as Luke confronts Darth Vader."),
new Movie(17, "Interstellar", 2014, "Astronauts travel through a wormhole to save a dying Earth."),
new Movie(18, "Parasite", 2019, "A poor family infiltrates a wealthy household with unforeseen fallout."),
new Movie(19, "Spirited Away", 2001, "A girl navigates a spirit bathhouse to free her parents."),
new Movie(20, "City of God", 2002, "Two boys take diverging paths amid Rio’s gang wars."),
new Movie(21, "Saving Private Ryan", 1998, "A squad risks everything to bring a paratrooper home."),
new Movie(22, "The Green Mile", 1999, "Death-row guards encounter a prisoner with miraculous gifts."),
new Movie(23, "Gladiator", 2000, "A betrayed general becomes Rome’s fiercest arena fighter."),
new Movie(24, "The Lion King", 1994, "An exiled lion cub returns to claim his destiny."),
new Movie(25, "Back to the Future", 1985, "A teen time-travels and risks erasing his own existence."),
new Movie(26, "The Departed", 2006, "An infiltrator and a mole play cat- and - mouse in Boston."),
new Movie(27, "Whiplash", 2014, "A jazz drummer endures a brutal mentor in pursuit of greatness."),
new Movie(28, "The Prestige", 2006, "Rival magicians wage a dangerous war of one - upmanship."),
new Movie(29, "The Usual Suspects", 1995, "A survivors’ tale hints at the legend of Keyser Söze."),
new Movie(30, "Terminator 2: Judgment Day", 1991, "A reprogrammed cyborg protects the future leader of humanity."),
new Movie(31, "Alien", 1979, "A crew is stalked by a lethal lifeform aboard a spaceship."),
new Movie(32, "Aliens", 1986, "Ripley returns to face a hive of xenomorphs on LV - 426."),
new Movie(33, "Blade Runner", 1982, "A detective hunts rogue androids in a neon - soaked future."),
new Movie(34, "Apocalypse Now", 1979, "A captain journeys upriver to terminate a renegade officer."),
new Movie(35, "One Flew Over the Cuckoo's Nest", 1975, "A rebel patient challenges a tyrannical nurse in a psych ward."),
new Movie(36, "Taxi Driver", 1976, "A disturbed NYC cabbie spirals toward violence."),
new Movie(37, "Oldboy", 2003, "A man seeks answers after 15 years of inexplicable captivity."),
new Movie(38, "Amélie", 2001, "A shy Parisian decides to secretly improve others’ lives."),
new Movie(39, "The Pianist", 2002, "A Jewish pianist struggles to survive Warsaw’s ghetto."),
new Movie(40, "American Beauty", 1999, "A suburban man’s midlife crisis upends his family."),
new Movie(41, "No Country for Old Men", 2007, "A stolen briefcase triggers relentless pursuit across Texas."),
new Movie(42, "There Will Be Blood", 2007, "An oilman’s ambition consumes everything around him."),
new Movie(43, "Mad Max: Fury Road", 2015, "A desert chase pits a warlord against a defiant road warrior."),
new Movie(44, "La La Land", 2016, "A musician and an actress chase dreams in modern LA."),
new Movie(45, "Joker", 2019, "A marginalized comedian’s breakdown sparks violent unrest."),
new Movie(46, "Avengers: Infinity War", 2018, "Earth’s heroes battle Thanos for the fate of half the universe."),
new Movie(47, "Avengers: Endgame", 2019, "Survivors attempt a time-heist toundo cosmic devastation."),
new Movie(48, "Toy Story", 1995, "Rivalry between a cowboy doll and a spaceranger turns to friendship."),
new Movie(49, "Inside Out", 2015, "A girl’s emotions guide her through a difficult move."),
new Movie(50, "The Social Network", 2010, "Facebook’s founding sparks friendship and legal battles.")
    });
    }

    public int NextMovieId() => ++nextMovieId;
    public int NextActorId() => ++nextActorId;
    public int NextUserId() => ++nextUserId;

    private void SeedActors()
    {
        Actors.AddRange(new Actor[]
        {
            new Actor(1,  "Al Pacino",            new DateTime(1940, 4,  25)),
            new Actor(2,  "Marlon Brando",         new DateTime(1924, 4,  3)),
            new Actor(3,  "Robert De Niro",        new DateTime(1943, 8,  17)),
            new Actor(4,  "Christian Bale",        new DateTime(1974, 1,  30)),
            new Actor(5,  "Heath Ledger",          new DateTime(1979, 4,  4)),
            new Actor(6,  "Morgan Freeman",        new DateTime(1937, 6,  1)),
            new Actor(7,  "Tim Robbins",           new DateTime(1958, 10, 16)),
            new Actor(8,  "John Travolta",         new DateTime(1954, 2,  18)),
            new Actor(9,  "Samuel L. Jackson",     new DateTime(1948, 12, 21)),
            new Actor(10, "Liam Neeson",           new DateTime(1952, 6,  7)),
            new Actor(11, "Elijah Wood",           new DateTime(1981, 1,  28)),
            new Actor(12, "Brad Pitt",             new DateTime(1963, 12, 18)),
            new Actor(13, "Edward Norton",         new DateTime(1969, 8,  18)),
            new Actor(14, "Tom Hanks",             new DateTime(1956, 7,  9)),
            new Actor(15, "Leonardo DiCaprio",     new DateTime(1974, 11, 11)),
            new Actor(16, "Keanu Reeves",          new DateTime(1964, 9,  2)),
            new Actor(17, "Jodie Foster",          new DateTime(1962, 11, 19)),
            new Actor(18, "Anthony Hopkins",       new DateTime(1937, 12, 31)),
            new Actor(19, "Mark Hamill",           new DateTime(1951, 9,  25)),
            new Actor(20, "Harrison Ford",         new DateTime(1942, 7,  13)),
            new Actor(21, "Matthew McConaughey",   new DateTime(1969, 11, 4)),
            new Actor(22, "Song Kang-ho",          new DateTime(1967, 1,  17)),
            new Actor(23, "Russell Crowe",         new DateTime(1964, 4,  7)),
            new Actor(24, "Joaquin Phoenix",       new DateTime(1974, 10, 28)),
            new Actor(25, "Robert Downey Jr.",     new DateTime(1965, 4,  4)),
            new Actor(26, "Tom Cruise",            new DateTime(1962, 7,  3)),
            new Actor(27, "Sigourney Weaver",      new DateTime(1949, 10, 8)),
            new Actor(28, "Jack Nicholson",        new DateTime(1937, 4,  22)),
            new Actor(29, "Dustin Hoffman",        new DateTime(1937, 8,  8)),
            new Actor(30, "Matt Damon",            new DateTime(1970, 10, 8)),
        });
    }

    private void SeedUsers()
    {
        Users.AddRange(new User[]
        {
            new User(1, "jeremie_dev", "jeremie@example.com"),
            new User(2, "profesor_inter", "profe@inter.edu")
        });
    }

    private void SeedActorsMovies()
    {
        ActorsMovies.AddRange(new ActorMovie[]
        {
            // The Godfather (1)
            new ActorMovie { Id = 1,  ActorId = 1,  MovieId = 1,  Role = "Michael Corleone" },
            new ActorMovie { Id = 2,  ActorId = 2,  MovieId = 1,  Role = "Vito Corleone" },
            // The Godfather Part II (2)
            new ActorMovie { Id = 3,  ActorId = 1,  MovieId = 2,  Role = "Michael Corleone" },
            new ActorMovie { Id = 4,  ActorId = 3,  MovieId = 2,  Role = "Young Vito Corleone" },
            // The Dark Knight (3)
            new ActorMovie { Id = 5,  ActorId = 4,  MovieId = 3,  Role = "Bruce Wayne / Batman" },
            new ActorMovie { Id = 6,  ActorId = 5,  MovieId = 3,  Role = "The Joker" },
            // The Shawshank Redemption (4)
            new ActorMovie { Id = 7,  ActorId = 7,  MovieId = 4,  Role = "Andy Dufresne" },
            new ActorMovie { Id = 8,  ActorId = 6,  MovieId = 4,  Role = "Ellis Boyd 'Red' Redding" },
            // Pulp Fiction (5)
            new ActorMovie { Id = 9,  ActorId = 8,  MovieId = 5,  Role = "Vincent Vega" },
            new ActorMovie { Id = 10, ActorId = 9,  MovieId = 5,  Role = "Jules Winnfield" },
            new ActorMovie { Id = 11, ActorId = 12, MovieId = 5,  Role = "Butch Coolidge" },
            // Schindler's List (6)
            new ActorMovie { Id = 12, ActorId = 10, MovieId = 6,  Role = "Oskar Schindler" },
            // The Lord of the Rings (7)
            new ActorMovie { Id = 13, ActorId = 11, MovieId = 7,  Role = "Frodo Baggins" },
            // Fight Club (8)
            new ActorMovie { Id = 14, ActorId = 13, MovieId = 8,  Role = "The Narrator" },
            new ActorMovie { Id = 15, ActorId = 12, MovieId = 8,  Role = "Tyler Durden" },
            // Forrest Gump (9)
            new ActorMovie { Id = 16, ActorId = 14, MovieId = 9,  Role = "Forrest Gump" },
            // Inception (10)
            new ActorMovie { Id = 17, ActorId = 15, MovieId = 10, Role = "Dom Cobb" },
            // The Matrix (11)
            new ActorMovie { Id = 18, ActorId = 16, MovieId = 11, Role = "Neo" },
            // Se7en (12)
            new ActorMovie { Id = 19, ActorId = 12, MovieId = 12, Role = "Detective Mills" },
            new ActorMovie { Id = 20, ActorId = 6,  MovieId = 12, Role = "Detective Somerset" },
            // Goodfellas (13)
            new ActorMovie { Id = 21, ActorId = 3,  MovieId = 13, Role = "James Conway" },
            // The Silence of the Lambs (14)
            new ActorMovie { Id = 22, ActorId = 17, MovieId = 14, Role = "Clarice Starling" },
            new ActorMovie { Id = 23, ActorId = 18, MovieId = 14, Role = "Hannibal Lecter" },
            // Star Wars IV (15)
            new ActorMovie { Id = 24, ActorId = 19, MovieId = 15, Role = "Luke Skywalker" },
            new ActorMovie { Id = 25, ActorId = 20, MovieId = 15, Role = "Han Solo" },
            // Interstellar (17)
            new ActorMovie { Id = 26, ActorId = 21, MovieId = 17, Role = "Cooper" },
            // Parasite (18)
            new ActorMovie { Id = 27, ActorId = 22, MovieId = 18, Role = "Ki-taek" },
            // Gladiator (23)
            new ActorMovie { Id = 28, ActorId = 23, MovieId = 23, Role = "Maximus" },
            // Joker (45)
            new ActorMovie { Id = 29, ActorId = 24, MovieId = 45, Role = "Arthur Fleck / Joker" },
            // Avengers: Endgame (47)
            new ActorMovie { Id = 30, ActorId = 25, MovieId = 47, Role = "Tony Stark / Iron Man" },
            // The Departed (26)
            new ActorMovie { Id = 31, ActorId = 15, MovieId = 26, Role = "Billy Costigan" },
            new ActorMovie { Id = 32, ActorId = 3,  MovieId = 26, Role = "Frank Costello" },
            new ActorMovie { Id = 33, ActorId = 30, MovieId = 26, Role = "Colin Sullivan" },
            // Alien (31)
            new ActorMovie { Id = 34, ActorId = 27, MovieId = 31, Role = "Ellen Ripley" },
            // One Flew Over the Cuckoo's Nest (35)
            new ActorMovie { Id = 35, ActorId = 28, MovieId = 35, Role = "R.P. McMurphy" },
            // The Graduate (social network adjacent) — The Social Network (50)
            new ActorMovie { Id = 36, ActorId = 29, MovieId = 9,  Role = "Lt. Dan Taylor" },
        });
    }
}