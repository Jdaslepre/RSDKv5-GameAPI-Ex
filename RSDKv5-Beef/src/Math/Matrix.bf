namespace RSDK;

[System.CRepr] public struct Matrix
{
    public int32[4][4] values = .();

    public this() { }
    public this(ref Self other) => Matrix.Copy(&this, &other);

    public void SetIdentity() mut                                                      => RSDKTable.SetIdentityMatrix(&this);
    public void TranslateXYZ(int32 x, int32 y, int32 z, bool32 setIdentity = true) mut => RSDKTable.MatrixTranslateXYZ(&this, x, y, z, setIdentity);
    public void ScaleXYZ(int32 x, int32 y, int32 z) mut                                => RSDKTable.MatrixScaleXYZ(&this, x, y, z);
    public void RotateX(int16 angle) mut                                               => RSDKTable.MatrixRotateX(&this, angle);
    public void RotateY(int16 angle) mut                                               => RSDKTable.MatrixRotateX(&this, angle);
    public void RotateZ(int16 angle) mut                                               => RSDKTable.MatrixRotateX(&this, angle);
    public void RotateXYZ(uint8 x, uint8 y, uint8 z) mut                               => RSDKTable.MatrixRotateXYZ(&this, x, y, z);
    public void Inverse() mut                                                          => RSDKTable.MatrixInverse(&this, &this);

    public static void Multiply(Self* dest, Self* matrixA, Self* matrixB)        => RSDKTable.MatrixMultiply(dest, matrixA, matrixB);
    public static void Copy(Self* matDest, Self* matSrc)                         => RSDKTable.MatrixCopy(matDest, matSrc);
    public static void Inverse(Self* dest, Self* matrix)                         => RSDKTable.MatrixInverse(dest, matrix);
}