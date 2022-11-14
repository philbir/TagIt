namespace TagIt.Tests;

[UsesVerify]
public class VerifyImageTest
{
    public VerifyImageTest()
    {
        VerifyImageMagick.Initialize();
        VerifyImageMagick.RegisterComparers(0.05);

        Verifier.DerivePathInfo(
            (sourceFile, projectDirectory, type, method) => new(
                directory: Path.Combine(
                    projectDirectory,
                    new FileInfo(sourceFile).DirectoryName ?? "",
                    "__verify__"),
                typeName: type.Name,
                methodName: method.Name));
    }

    public virtual CancellationToken TestCanceled
        => new CancellationTokenSource(TimeSpan.FromSeconds(30))
            .Token;
}
