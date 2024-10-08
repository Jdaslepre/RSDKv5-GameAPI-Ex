using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe struct Matrix
    {
        public Matrix() { }
        public Matrix(RSDK.Matrix other) { }

        public int[,] values = new int[4, 4];

        public void SetIdentity() { fixed (RSDK.Matrix* m = &this) { RSDKTable.SetIdentityMatrix(m); } }
        public void TranslateXYZ(int x, int y, int z, uint setIdentity = 1) { fixed (RSDK.Matrix* m = &this) RSDKTable.MatrixTranslateXYZ(m, x, y, z, setIdentity); }
        public void ScaleXYZ(int x, int y, int z) { fixed (RSDK.Matrix* m = &this) RSDKTable.MatrixScaleXYZ(m, x, y, z); }
        public void RotateX(int angle) { fixed (RSDK.Matrix* m = &this) RSDKTable.MatrixRotateX(m, (short)angle); }
        public void RotateY(int angle) { fixed (RSDK.Matrix* m = &this) RSDKTable.MatrixRotateX(m, (short)angle); }
        public void RotateZ(int angle) { fixed (RSDK.Matrix* m = &this) RSDKTable.MatrixRotateX(m, (short)angle); }
        public void RotateXYZ(int x, int y, int z) { fixed (RSDK.Matrix* m = &this) { RSDKTable.MatrixRotateXYZ(m, (short)x, (short)y, (short)z); } }
        public void Inverse() { fixed (RSDK.Matrix* m = &this) RSDKTable.MatrixInverse(m, m); }

        public static void Multiply(ref Matrix dest, ref Matrix matrixA, ref Matrix matrixB)
        {
            // man.
            fixed (Matrix* d = &dest)
            fixed (Matrix* a = &matrixA)
            fixed (Matrix* b = &matrixB)
                RSDKTable.MatrixMultiply(d, a, b);
        }
        public static void Copy(RSDK.Matrix* matDest, RSDK.Matrix* matSrc) => RSDKTable.MatrixCopy(matDest, matSrc);
        public static void Inverse(RSDK.Matrix* dest, RSDK.Matrix* matrix) => RSDKTable.MatrixInverse(dest, matrix);

        /*
        inline Matrix &operator*=(Matrix &other)
        {
            Matrix::Multiply(this, this, &other);
            return *this;
        }

        friend inline Matrix operator*(Matrix &lhs, Matrix &rhs)
        {
            Matrix dest;
            Matrix::Multiply(&dest, &lhs, &rhs);
            return dest;
        }
        */
    }
}
