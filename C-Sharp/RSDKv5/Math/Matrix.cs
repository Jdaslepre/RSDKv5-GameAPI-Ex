namespace RSDK;

public unsafe struct Matrix
{
    public Matrix() { }
    public Matrix(Matrix other) { }

    public int[,] values = new int[4, 4];

    public void SetIdentity() => RSDKTable.SetIdentityMatrix(ref this);
    public void TranslateXYZ(int x, int y, int z, uint setIdentity = 1) => RSDKTable.MatrixTranslateXYZ(ref this, x, y, z, setIdentity);
    public void ScaleXYZ(int x, int y, int z) => RSDKTable.MatrixScaleXYZ(ref this, x, y, z);
    public void RotateX(int angle) => RSDKTable.MatrixRotateX(ref this, (short)angle);
    public void RotateY(int angle) => RSDKTable.MatrixRotateY(ref this, (short)angle);
    public void RotateZ(int angle) => RSDKTable.MatrixRotateZ(ref this, (short)angle);
    public void RotateXYZ(int x, int y, int z) => RSDKTable.MatrixRotateXYZ(ref this, (short)x, (short)y, (short)z);
    public void Inverse() => RSDKTable.MatrixInverse(ref this, ref this);

    public static void Multiply(ref Matrix dest, ref Matrix matrixA, ref Matrix matrixB) => RSDKTable.MatrixMultiply(ref dest, ref matrixA, ref matrixB);
    public static void Copy(ref Matrix matDest, ref Matrix matSrc) => RSDKTable.MatrixCopy(ref matDest, ref matSrc);
    public static void Inverse(ref Matrix dest, ref Matrix matrix) => RSDKTable.MatrixInverse(ref dest, ref matrix);

    /*
    inline Matrix &operator*=(Matrix &other)
    {
        Matrix::Multiply(this, this, &other);
        return *this;
    }
    */

    public static Matrix operator *(Matrix lhs, Matrix rhs)
    {
        Matrix dest = new();
        Multiply(ref dest, ref lhs, ref rhs);
        return dest;
    }
}