﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LispEngine.Datums;
using LispEngine.Evaluation;
using Environment = LispEngine.Evaluation.Environment;

namespace LispEngine.Core
{
    class Quote : AbstractFExpression
    {
        public static readonly FExpression Instance = new Quote();

        private static Datum evaluate(Continuation c, Datum args)
        {
            var argList = args.ToArray();
            if (argList.Length != 1)
                throw c.error("invalid syntax '{0}'", args);
            return argList[0];            
        }

        public override Continuation Evaluate(Continuation c, Environment env, Datum args)
        {
            return c.PushResult(evaluate(c, args));
        }
    }
}
