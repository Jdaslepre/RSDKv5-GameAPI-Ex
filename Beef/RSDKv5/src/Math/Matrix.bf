namespace RSDK;

public struct Matrix
{
    public int32[4][4] values = .();

    public this() { }
    public this(ref RSDK.Matrix other) => Matrix.Copy(&this, &other);

    public void SetIdentity() mut => RSDKTable.SetIdentityMatrix(&this);
    public void TranslateXYZ(int32 x, int32 y, int32 z, bool setIdentity = true) mut => RSDKTable.MatrixTranslateXYZ(&this, x, y, z, setIdentity);
    public void ScaleXYZ(int32 x, int32 y, int32 z) mut => RSDKTable.MatrixScaleXYZ(&this, x, y, z);
    public void RotateX(int16 angle) mut => RSDKTable.MatrixRotateX(&this, angle);
    public void RotateY(int16 angle) mut => RSDKTable.MatrixRotateX(&this, angle);
    public void RotateZ(int16 angle) mut => RSDKTable.MatrixRotateX(&this, angle);
    public void RotateXYZ(uint8 x, uint8 y, uint8 z) mut => RSDKTable.MatrixRotateXYZ(&this, x, y, z);
    public void Inverse() mut => RSDKTable.MatrixInverse(&this, &this);

    public static void Multiply(RSDK.Matrix* dest, RSDK.Matrix* matrixA, RSDK.Matrix* matrixB) => RSDKTable.MatrixMultiply(dest, matrixA, matrixB);
    public static void Copy(RSDK.Matrix* matDest, RSDK.Matrix* matSrc) => RSDKTable.MatrixCopy(matDest, matSrc);
    public static void Inverse(RSDK.Matrix* dest, RSDK.Matrix* matrix) => RSDKTable.MatrixInverse(dest, matrix);
}