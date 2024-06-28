using System.Collections.Generic;
using System.Collections.Immutable;

namespace Solution;

/// <summary>
/// Simple assembler interpreter
/// https://www.codewars.com/kata/58e24788e24ddee28e000053/train/csharp
/// </summary>
public static class SimpleAssembler
{
    public static Dictionary<string, int> Interpret(string[] program)
    {
        var machine = Machine.Create(program);
        machine.Run();

        return new Dictionary<string, int>(machine.ExposeRegisters());
    }
}

internal class Machine
{
    private int _programCounter;
    private readonly IDictionary<string, int> _registers = new Dictionary<string, int>();
    private readonly List<Instruction> _instructions;

    private Machine(List<Instruction> instructions)
    {
        _instructions = instructions;
    }

    public void Run()
    {
        while (_programCounter >= 0 && _programCounter < _instructions.Count)
        {
            _instructions[_programCounter].Execute(this);
            _programCounter++;
        }
    }

    public void Jump(int offset)
    {
        _programCounter += offset;
    }

    public void StoreRegister(string register, int value)
    {
        _registers[register] = value;
    }

    public int ReadRegister(string register)
    {
        return _registers[register];
    }

    public static Machine Create(string[] program)
    {
        var instructions = Instruction.Parse(program);
        return new Machine(instructions);
    }

    public IDictionary<string, int> ExposeRegisters()
    {
        return _registers.ToImmutableDictionary();
    }
}

internal interface Instruction
{
    void Execute(Machine machine);

    static List<Instruction> Parse(string[] program)
    {
        var instructions = new List<Instruction>();

        foreach (var line in program)
        {
            var tokens = line.Split(' ');
            var instruction = tokens[0];
            var x = tokens[1];
            var y = tokens.Length == 3 ? tokens[2] : null;

            switch (instruction)
            {
                case "mov":
                    instructions.Add(Move.From(x, y));
                    break;
                case "inc":
                    instructions.Add(Increment.From(x));
                    break;
                case "dec":
                    instructions.Add(Decrement.From(x));
                    break;
                case "jnz":
                    instructions.Add(JumpIfNotZero.From(x, y));
                    break;
            }
        }

        return instructions;
    }
}

internal class Move : Instruction
{
    private readonly string _register;

    private readonly int? _value;
    private readonly string _sourceRegister;

    private Move(string register, int value)
    {
        _register = register;
        _value = value;
    }

    private Move(string register, string sourceRegister)
    {
        _register = register;
        _sourceRegister = sourceRegister;
    }

    public void Execute(Machine machine)
    {
        var actualValue = _value ?? machine.ReadRegister(_sourceRegister);
        machine.StoreRegister(_register, actualValue);
    }

    public static Instruction From(string x, string y)
    {
        return int.TryParse(y, out var yValue)
            ? new Move(x, yValue)
            : new Move(x, y);
    }
}

internal class Increment : Instruction
{
    private readonly string _register;

    private Increment(string register)
    {
        _register = register;
    }

    public void Execute(Machine machine)
    {
        machine.StoreRegister(_register, machine.ReadRegister(_register) + 1);
    }

    public static Instruction From(string x)
    {
        return new Increment(x);
    }
}

internal class Decrement : Instruction
{
    private readonly string _register;

    private Decrement(string register)
    {
        _register = register;
    }

    public void Execute(Machine machine)
    {
        machine.StoreRegister(_register, machine.ReadRegister(_register) - 1);
    }

    public static Instruction From(string x)
    {
        return new Decrement(x);
    }
}

internal class JumpIfNotZero : Instruction
{
    private readonly int? _indicator;
    private readonly string _indicatorRegister;

    private readonly int? _offset;
    private readonly string _offsetRegister;

    private JumpIfNotZero(string indicatorRegister, int offset)
    {
        _indicatorRegister = indicatorRegister;
        _offset = offset;
    }

    private JumpIfNotZero(string indicatorRegister, string offsetRegister)
    {
        _indicatorRegister = indicatorRegister;
        _offsetRegister = offsetRegister;
    }

    private JumpIfNotZero(int indicator, int offset)
    {
        _indicator = indicator;
        _offset = offset;
    }

    private JumpIfNotZero(int indicator, string offsetRegister)
    {
        _indicator = indicator;
        _offsetRegister = offsetRegister;
    }

    public void Execute(Machine machine)
    {
        var actualIndicator = _indicator ?? machine.ReadRegister(_indicatorRegister);

        if (actualIndicator != 0)
        {
            var actualOffset = _offset ?? machine.ReadRegister(_offsetRegister);
            machine.Jump(actualOffset - 1);
        }
    }

    public static Instruction From(string x, string y)
    {
        var parsedY = int.TryParse(y, out var yValue);

        return int.TryParse(x, out var xValue)
            ? parsedY
                ? new JumpIfNotZero(xValue, yValue)
                : new JumpIfNotZero(xValue, y)
            : parsedY
                ? new JumpIfNotZero(x, yValue)
                : new JumpIfNotZero(x, y);
    }
}
