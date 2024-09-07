# Calculator

A project implementing a calculator able to parse complex mathematical expressions.

Can optionally log the solve steps, e.g.

```
(5 + 1) * (8 - 2)
6 * (8 - 2)
6 * 6
36
```

```
6 / 2 * (2 + 1)
3 * (2 + 1)
3 * 3
9
```

```csharp
// Pass in null to Evaluator if no step logging requiured.
var evaluator = new Evaluator(new EvaluationLogger(new ConsoleOutputProvider()));

var result = evaluator.Evaluate("6 / 2 * (2 + 1)");
```