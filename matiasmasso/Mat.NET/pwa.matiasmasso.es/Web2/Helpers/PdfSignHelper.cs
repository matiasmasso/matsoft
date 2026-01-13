using iText.Bouncycastle.X509;
using iText.Bouncycastle.Crypto;
using iText.Commons.Bouncycastle.Cert;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Pkcs;
using Rectangle = iText.Kernel.Geom.Rectangle;
using DTO;
using iText.IO.Image;

namespace Web.Helpers
{
    public class PdfSignHelper
    {

        public static byte[] Sign(byte[] srcBytes, CertModel? cert, int x, int y, int width, int height)
        {
            if (cert?.HasValue() ?? false)
            {
                //Read the PFX/P12 certificate file
                var certMemoryStream = new MemoryStream(cert.Data!.Data!);
                char[] password = cert.Password!.ToCharArray();
                Pkcs12Store pk12 = new Pkcs12Store(certMemoryStream, password);

                string? alias = null;
                foreach (var a in pk12.Aliases)
                {
                    alias = ((string)a);
                    if (pk12.IsKeyEntry(alias))
                        break;
                }

                //build the certificate chain
                ICipherParameters pk = pk12.GetKey(alias).Key;
                X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
                X509Certificate[] chain = new X509Certificate[ce.Length];
                for (int k = 0; k < ce.Length; ++k)
                {
                    chain[k] = ce[k].Certificate;
                }

                //read the source
                var srcMemoryStream = new MemoryStream(srcBytes);
                var destMemoryStream = new MemoryStream();
                PdfReader reader = new PdfReader(srcMemoryStream);
                PdfSigner signer = new PdfSigner(reader, destMemoryStream, new StampingProperties());


                //set the signature field
                Rectangle rect = new Rectangle(x, y, width, height);
                PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
                appearance.SetLocation("Barcelona").SetReuseAppearance(false).SetPageRect(rect).SetPageNumber(1);
                if (cert.Image?.Data == null)
                    appearance.SetRenderingMode(PdfSignatureAppearance.RenderingMode.DESCRIPTION);
                else
                {
                    ImageData iTextImage = ImageDataFactory.Create(cert.Image.Data);
                    var originalWidth = iTextImage.GetWidth();
                    var originalHeight = iTextImage.GetHeight();
                    var isLandscape = originalWidth / rect.GetWidth() > originalHeight / rect.GetHeight();
                    float scaler = isLandscape ? originalWidth / rect.GetWidth() : originalHeight / rect.GetHeight();
                    appearance.SetImageScale(100); // scales image into destination rectangle
                    appearance.SetSignatureGraphic(iTextImage);
                    appearance.SetRenderingMode(PdfSignatureAppearance.RenderingMode.GRAPHIC);

                    

                }

                IExternalSignature pks = new PrivateKeySignature(new PrivateKeyBC(pk), DigestAlgorithms.SHA256);

                IX509Certificate[] certificateWrappers = new IX509Certificate[chain.Length];
                for (int i = 0; i < certificateWrappers.Length; ++i)
                {
                    certificateWrappers[i] = new X509CertificateBC(chain[i]);
                }

                // Sign the document using the detached mode, CMS or CAdES equivalent.
                signer.SignDetached(pks, certificateWrappers, null, null, null, 0, PdfSigner.CryptoStandard.CMS);

                return destMemoryStream.ToArray();
            }
            else
                return srcBytes;
        }

    }
}

