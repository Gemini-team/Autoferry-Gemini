using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;

public static class RustInterop {

    [StructLayout(LayoutKind.Sequential)]
    struct ArrayRef
    {
        public IntPtr Bytes;
        public uint Length;
    }

    [DllImport("unity_rust")]
    private static extern int get_random_int();

    [DllImport("unity_rust")]
    private static extern int multiply_by_two(int num);

    [DllImport("unity_rust")]
    private static extern int add_x_to_each_element(ArrayRef array, uint count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRandomInt()
    {
        return get_random_int();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MultiplyByTwo(int num)
    {
        return multiply_by_two(num);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static int AddXToEachElement(byte[] array, uint length)
    {
        //return add_x_to_each_element(x, arr_ptr, length);
        ArrayRef arrayRef = new ArrayRef();

        IntPtr ptr;

        fixed (byte* p = array)
        {
            ptr = (IntPtr)p;
        }

        arrayRef.Bytes = ptr;
        arrayRef.Length = length;

        return add_x_to_each_element(arrayRef, arrayRef.Length);
        
        

    }
}
