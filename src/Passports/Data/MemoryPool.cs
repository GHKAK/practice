namespace Passports.Data; 


public class MemoryPool {
    private Stack<Memory<byte>> _availableMemories;

    public MemoryPool(int poolCapacity, int memorySize) {
        _availableMemories = new();
        for (int i = 0; i < poolCapacity; i++) {
            _availableMemories.Push(new Memory<byte>(new byte[memorySize]));
        }
    }

    public Memory<byte> GetMemory() {
        return _availableMemories.Pop();
    }

    public void ReturnMemory(Memory<byte> memory) {
        _availableMemories.Push(memory);
    }
}