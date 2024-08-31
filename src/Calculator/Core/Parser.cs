namespace Calculator.Core;

public class Parser
{
    private readonly Queue<Element> _queue = new();

    private readonly Stack<string> _stack = new();

    private int _position;

    private string _expression;

    private TokenType _previousTokenType;

    private string _previousSymbol;

    public Queue<Element> Parse(string expression)
    {
        Initialise(expression);
        
        while (_position < _expression.Length)
        {
            if (ProcessForIgnored())
            {
                continue;
            }
            
            if (ProcessForNumbers())
            {
                _previousTokenType = TokenType.Number;
                
                continue;
            }

            if (ProcessForFunctions())
            {
                _previousTokenType = TokenType.Function;
                
                continue;
            }

            if (ProcessForParentheses())
            {
                _previousTokenType = TokenType.Parenthesis;
                
                continue;
            }

            ProcessForOperators();

            _previousTokenType = TokenType.Operator;
        }

        while (_stack.Count > 0)
        {
            _queue.Enqueue(Element.Create(_stack.Pop()));
        }

        return new Queue<Element>(_queue);
    }

    private void Initialise(string expression)
    {
        _queue.Clear();

        _stack.Clear();

        _position = 0;

        _expression = expression;

        _previousTokenType = TokenType.None;

        _previousSymbol = string.Empty;
    }

    private bool ProcessForIgnored()
    {
        if (char.IsWhiteSpace(_expression[_position]) || _expression[_position] == ',')
        {
            _position++;

            return true;
        }

        return false;
    }

    private bool ProcessForNumbers()
    {
        if (_expression[_position] == 'π')
        {
            _queue.Enqueue(Element.Create(Math.PI));
            
            _position++;
            
            return true;
        }

        if (! (char.IsDigit(_expression[_position]) || _expression[_position] == '.'))
        {
            return false;
        }

        var start = _position;

        while (_position < _expression.Length && (char.IsDigit(_expression[_position]) || _expression[_position] == '.'))
        {
            _position++;
        }

        var number = double.Parse(_expression.Substring(start, _position - start));

        _queue.Enqueue(Element.Create(number));

        return true;
    }
    
    private bool ProcessForFunctions()
    {
        if (! char.IsLetter(_expression[_position]))
        {
            return false;
        }

        var start = _position;

        while (_position < _expression.Length && char.IsLetter(_expression[_position]))
        {
            _position++;
        }

        var function = _expression.Substring(start, _position - start);
        
        _stack.Push(function);

        return true;
    }

    private bool ProcessForParentheses()
    {
        switch (_expression[_position])
        {
            case '(':
                _stack.Push("(");

                _position++;
                
                return true;

            case ')':
            {
                while (_stack.Count > 0 && _stack.Peek() != "(")
                {
                    _queue.Enqueue(Element.Create(_stack.Pop()));
                }

                if (_stack.Peek() == "(")
                {
                    _stack.Pop();
                }

                if (_stack.Count > 0 && char.IsLetter(_stack.Peek()[0]))
                {
                    _queue.Enqueue(Element.Create(_stack.Pop()));
                }

                _position++;

                return true;
            }
        }

        return false;
    }

    private void ProcessForOperators()
    {
        var symbol = _expression[_position].ToString();
        
        if (_previousTokenType != TokenType.Number && symbol == "-" && _previousSymbol != "!")
        {
            symbol = "--";
        }

        if (symbol is "<" or ">")
        {
            _position++;
            
            symbol = $"{symbol}{_expression[_position]}";
        }

        _previousSymbol = symbol;

        var precedence = GetPrecedence(symbol);

        if (_stack.Count > 0)
        {
            var top = _stack.Peek();

            while (_stack.Count > 0 && top != "(" && (GetPrecedence(top) > precedence || (GetPrecedence(top) == precedence && IsLeftAssociative(symbol))))
            {
                _queue.Enqueue(Element.Create(_stack.Pop()));

                if (_stack.Count > 0)
                {
                    top = _stack.Peek();
                }
            }
        }

        _stack.Push(symbol);

        _position++;
    }

    private static bool IsLeftAssociative(string symbol)
    {
        return symbol != "^";
    }

    private static int GetPrecedence(string symbol)
    {
        return symbol switch
        {
            "!" => 4,
            "--" => 4,
            "^" => 3,
            "*" => 2,
            "/" => 2,
            "%" => 2,
            "+" => 1,
            "-" => 1,
            _ => 0
        };
    }
}