﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LispEngine.Datums;
using LispEngine.Evaluation;
using Environment = LispEngine.Evaluation.Environment;

namespace LispEngine.Core
{
    class Define : AbstractFExpression
    {
        public static readonly FExpression Instance = new Define();

        class DefineName : Task
        {
            private readonly Environment env;
            private readonly string name;
            public DefineName(Environment env, string name)
            {
                this.env = env;
                this.name = name;
            }

            public Continuation Perform(Continuation c)
            {
                env.Define(name, c.Result);
                return c;
            }

            public override string ToString()
            {
                return string.Format("Define name '{0}'", name);
            }
        }

        public override Continuation Evaluate(Continuation c, Environment env, Datum args)
        {
            var argList = args.ToArray();
            if (argList.Length != 2)
                throw c.error("Expected 2 arguments: (define <symbol> <expression>). Got {0} instead", argList.Length);
            var name = argList[0].CastIdentifier();
            var expression = argList[1];
            c = c.PushTask(new DefineName(env, name));
            return c.Evaluate(env, expression);
        }
    }
}
