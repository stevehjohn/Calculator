namespace Calculator.Core;

public class Parser
{
    private readonly Queue<Element> _queue = new();

    private readonly Stack<char> _stack = new();

    private int _position;

    private string _expression;

    public Queue<Element> Parse(string expression)
    {
        Initialise(expression);
        
        while (_position < _expression.Length)
        {
            if (char.IsWhiteSpace(_expression[_position]))
            {
                _position++;

                continue;
            }
            
            if (ProcessForNumbers())
            {
                continue;
            }
            
            if (ProcessForParentheses())
            {
                continue;
            }

            ProcessForOperators();
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
    }

    private bool ProcessForNumbers()
    {
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

    private bool ProcessForParentheses()
    {
        switch (_expression[_position])
        {
            case '(':
                _stack.Push(_expression[_position]);

                _position++;
                
                return true;

            case ')':
            {
                while (_stack.Count > 0 && _stack.Peek() != '(')
                {
                    _queue.Enqueue(Element.Create(_stack.Pop()));
                }

                if (_stack.Peek() == '(')
                {
                    _stack.Pop();
                }

                _position++;

                return true;
            }
        }

        return false;
    }

    private void ProcessForOperators()
    {
        var precedence = GetPrecedence(_expression[_position]);

        if (_stack.Count > 0)
        {
            var top = _stack.Peek();

            while (_stack.Count > 0 && top != '(' && (GetPrecedence(top) > precedence || (GetPrecedence(top) == precedence && _expression[_position] != '^')))
            {
                _queue.Enqueue(Element.Create(_stack.Pop()));

                if (_stack.Count > 0)
                {
                    top = _stack.Peek();
                }
            }
        }

        _stack.Push(_expression[_position]);

        _position++;
    }

    private static int GetPrecedence(char symbol)
    {
        return symbol switch
        {
            '^' => 2,
            '*' => 1,
            '/' => 1,
            _ => 0
        };
    }
}