using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assets.Scripts.MazeSolver
{
    public class GeneticAlg
    {
        private Bot[] pool;
        private int[] scores;
        private int[] order;
        public Maze maze;
        public int generation;

        public GeneticAlg(int win_w, int win_h)
        {
            generation = 0;
            maze = new Maze(MazeManager.settings.maze_width, MazeManager.settings.maze_height, win_w, win_h);
            pool = new Bot[MazeManager.settings.bot_number];
            scores = new int[MazeManager.settings.bot_number];
            order = new int[MazeManager.settings.bot_number];

            for (int i = 0; i < MazeManager.settings.bot_number; ++i)
                pool[i] = new Bot(ref maze);
        }

        private int ScoreBot(CoordI grid_pos, double energy)
        {
            var result = 0;

            var learnDistance = MazeManager.settings.learn_distance;

            if (learnDistance && maze.walls.in_bounds(grid_pos))
                result += maze.walls.get_path_value(grid_pos);
            else if (learnDistance)
                result += 1000;

            result -= (int)energy;

            return result;
        }


        /// <summary>
        /// Move on to the next generation.
        /// Scores the bots and then kills the worst third of the population,
        /// breeding from the upper two thirds to refill the population.
        /// Regenerates the maze and moves the bots to the start.
        /// </summary>
        public void new_generation()
        {
            var thirdOfPool = (pool.Length - 1) / 3;

            var scoredBots = pool
                .Select((bot, pos) => new
                {
                    Bot = bot,
                    Score = ScoreBot(bot.grid_pos, pos)
                })
                .OrderBy(x => x.Score)
                .ToList();



            var withChildOverwritten = scoredBots.Select(
                (x, pos) =>
                {
                    if (pos < thirdOfPool)
                        x.Bot.brain.breed(scoredBots[pos + thirdOfPool].Bot.brain, scoredBots[pos + 2 * thirdOfPool].Bot.brain, 0.1);

                    return x.Bot;
                });

            pool = withChildOverwritten.ToArray();

            ++generation;
            maze.generate();

            for (int i = 0; i < pool.Length; ++i)
                pool[i].init(maze.start * maze.cell_size + (maze.cell_size / 2.0));
        }

        public void update(PaintEventArgs e)
        {
            maze.draw(e);

            if (pool.Length == 0) return;

            bool done = true;

            for (int i = 0; i < pool.Length; ++i)
            {
                done &= !pool[i].alive; // check if all dead
                pool[i].update_eyes(maze);
                pool[i].think();
                pool[i].update_eyes(maze);
                pool[i].draw(e);
            }

            if (done) new_generation();
        }
    }
}
