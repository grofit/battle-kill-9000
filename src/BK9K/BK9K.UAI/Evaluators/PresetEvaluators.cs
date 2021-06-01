namespace BK9K.UAI.Evaluators
{
    public class PresetEvaluators
    {
        public static IEvaluator Constant = new PolynomialEvaluator(0f, 0, 0.5f, 0);
        public static IEvaluator Linear = new PolynomialEvaluator(1.0f, 0, 0, 1.0f);
        public static IEvaluator InverseLinear = new PolynomialEvaluator(-1.0f, 1.0f, 0, 1.0f);
        public static IEvaluator StandardCooldown = new PolynomialEvaluator(1.0f, 0f, 0, 6.0f);
        public static IEvaluator StandardRuntime = new PolynomialEvaluator(-1.0f, 0f, 1.0f, 6.0f);
        public static IEvaluator QuadraticLowerLeft = new PolynomialEvaluator(1.0f, 1.0f, 0.0f, 4.0f);
        public static IEvaluator QuadraticLowerRight = new PolynomialEvaluator(1.0f, 0.0f, 0.0f, 4.0f);
        public static IEvaluator QuadraticUpperLeft = new PolynomialEvaluator(-1.0f, 1.0f, 1.0f, 4.0f);
        public static IEvaluator QuadraticUpperRight = new PolynomialEvaluator(-1.0f, 0f, 1.0f, 4.0f);
        public static IEvaluator Logistic = new LogisticEvaluator(1.0f, 0f, 0.0f, 1.0f);
        public static IEvaluator InverseLogistic = new LogisticEvaluator(-1.0f, 0f, 1.0f, 1.0f);
        public static IEvaluator Logit = new LogitEvaluator(1.0f, 0, 0);
        public static IEvaluator InverseLogit = new LogitEvaluator(-1.0f, 0, 0);
        public static IEvaluator BellCurve = new NormalEvaluator(1.0f, 0, 0, 1.0f);
        public static IEvaluator InverseBellCurve = new NormalEvaluator(-1.0f, 0, 1.0f, 1.0f);
        public static IEvaluator SineWave = new SineEvaluator(1.0f, 0, 0);
        public static IEvaluator InverseSineWave = new SineEvaluator(-1.0f, 0, 0);
        public static IEvaluator GreaterThanHalf = new StepEvaluator(0.5f);
        public static IEvaluator LessThanHalf = new StepEvaluator(0.5f, 1.0f, 0.0f);
        public static IEvaluator PassThrough = new PassThroughlEvaluator();
    }
}