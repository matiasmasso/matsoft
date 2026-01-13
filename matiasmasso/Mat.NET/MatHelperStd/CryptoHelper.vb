Imports System.Security.Cryptography


Public Class CryptoHelper


    'Shared Function HashMD5(ByVal sCadena As String) As String
    '    Dim ueCodigo As New System.Text.UnicodeEncoding()
    '    Dim oByteArray As Byte() = ueCodigo.GetBytes(sCadena)
    '    Dim retval As String = HashMD5(oByteArray)
    '    Return retval
    'End Function

    Shared Function HashMD5(oByteArray As Byte()) As String
        Dim Md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
        'Compute the hash value from the source
        Dim ByteHash() As Byte = Md5.ComputeHash(oByteArray)
        'And convert it to String format for return
        Dim retval As String = Convert.ToBase64String(ByteHash)
        Return retval
    End Function

    Shared Function FromUrFriendlyBase64(source As String) As String
        Dim retval As String = source.Replace("-", "+").Replace("_", "/").Replace("~", "=")
        Return retval
    End Function

    Shared Function UrlFriendlyBase64(source As String) As String
        Dim retval As String = ""
        If Not String.IsNullOrEmpty(source) Then
            retval = source.Replace("+", "-").Replace("/", "_").Replace("=", "~")
        End If
        Return retval
    End Function


    Shared Function UrlFriendlyBase64Json(oParameters As Dictionary(Of String, String)) As String
        Dim sJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(oParameters)
        Dim oBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(sJson)
        Dim sBase64 As String = System.Convert.ToBase64String(oBytes)
        Dim retval As String = UrlFriendlyBase64(sBase64)
        Return retval
    End Function

    Shared Function FromUrlFriendlyBase64Json(source As String) As Dictionary(Of String, String)
        Dim sFromUrlFriendly As String = FromUrFriendlyBase64(source)
        'Dim padded = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
        Dim oBytes() As Byte = System.Convert.FromBase64String(sFromUrlFriendly)
        Dim sDecodedSource As String = System.Text.Encoding.UTF8.GetString(oBytes)
        Dim o As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(sDecodedSource)
        Dim retval As Dictionary(Of String, String) = o.ToObject(Of Dictionary(Of String, String))
        Return retval
    End Function


    Shared Function StringToHexadecimal(Src As String) As String
        Dim oByteArray() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(Src)
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder

        For i As Integer = 0 To oByteArray.Length - 1
            sb.Append(oByteArray(i).ToString("x"))
        Next

        Dim retval As String = sb.ToString()
        Return retval
    End Function

    Shared Function HexToString(ByVal Src As String) As String
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 0 To Src.Length - 1 Step 2
            Dim SrcPair As String = Src.Substring(i, 2)
            Dim Hex As Integer = CInt("&H" & SrcPair)
            Dim sChar As String = ChrW(Hex)
            sb.Append(sChar)
        Next

        Dim retval As String = sb.ToString()
        Return retval
    End Function

    Shared Function HashString(inputString As String, hashName As String, exs As List(Of Exception)) As String
        Dim algorithm As Security.Cryptography.HashAlgorithm = Security.Cryptography.HashAlgorithm.Create(hashName)
        If algorithm Is Nothing Then
            exs.Add(New Exception("Unrecognized hash name " & hashName))
        End If
        Dim hash As Byte() = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputString))
        Return Convert.ToBase64String(hash)
    End Function

    Shared Function GetSha256Hash(src As String) As String
        Dim oSha256 As Security.Cryptography.SHA256Managed = Security.Cryptography.SHA256Managed.Create()
        Dim hash As Byte() = oSha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(src))
        Dim retval As String = Convert.ToBase64String(hash)
        Return retval
    End Function

    Shared Function Sha256(bytes As Byte()) As String
        Dim oSha256 As Security.Cryptography.SHA256Managed = Security.Cryptography.SHA256Managed.Create()
        Dim hash As Byte() = oSha256.ComputeHash(bytes)
        Dim retval As String = Convert.ToBase64String(hash)
        Return retval
    End Function

    Shared Function GetHMACSHA256(msg As String, SignatureKey As Byte()) As Byte()
        Dim retval As Byte() = Nothing
        Try
            'Obtain byte[] from input string
            Dim msgBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(msg)

            'Initialize the keyed hash object.
            Using hmac As New HMACSHA256(SignatureKey)

                'Compute the hash of the input file.
                retval = hmac.ComputeHash(msgBytes, 0, msgBytes.Length)
            End Using
        Catch ex As CryptographicException
            Throw New CryptographicException(ex.Message)
        End Try
        Return retval
    End Function

    Shared Function Encrypt3DES(plainText As String, SignatureKey As Byte()) As Byte()
        Dim retval As Byte() = Nothing

        If String.IsNullOrEmpty(plainText) Then
            Throw New FormatException("pedido vacio en Encrypt3DES")
        Else
            Dim toEncryptArray As Byte() = System.Text.Encoding.UTF8.GetBytes(plainText)
            Dim tdes As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
            Try
                ' SALT used in 3DES encryptation process
                Dim SALT As Byte() = {0, 0, 0, 0, 0, 0, 0, 0}

                ' Block size 64 bit (8 bytes)
                tdes.BlockSize = 64

                ' Key Size 192 bit (24 bytes)
                tdes.KeySize = 192
                tdes.Mode = CipherMode.CBC
                tdes.Padding = PaddingMode.Zeros

                tdes.IV = SALT
                tdes.Key = SignatureKey

                Dim cTransform As ICryptoTransform = tdes.CreateEncryptor()

                'transform the specified region of bytes array to resultArray
                retval = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)

                'Release resources held by TripleDes Encryptor
                tdes.Clear()

            Catch ex As CryptographicException
                Throw New CryptographicException(ex.Message)
            End Try

        End If

        Return retval
    End Function


    Shared Function ToBase64(src As String) As String
        Dim oBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(src)
        Dim retval As String = ToBase64(oBytes)
        Return retval
    End Function

    Shared Function ToBase64(oBytes As Byte()) As String
        Dim retval As String = Convert.ToBase64String(oBytes)
        Return retval
    End Function

    Shared Function FromBase64(src As String) As String
        Dim oBytes As Byte() = Convert.FromBase64String(src)
        Dim retval As String = System.Text.Encoding.UTF8.GetString(oBytes)
        Return retval
    End Function


    Shared Function Encode62Nums(srcs As IEnumerable(Of Integer)) As String
        Dim sb As New Text.StringBuilder
        For Each src In srcs
            sb.Append(Encode62Num(src))
        Next
        Return sb.ToString
    End Function

    Shared Function Encode62Num(src As Integer) As String
        'returns a 3 char alfanumeric from integer lower than 62^3 using 10 decimal digits, 26 uppercase and 26 lowercase letters
        Dim i1 = Math.Floor(src / 62 / 62)
        Dim i2 = Math.Floor((src - 62 * 62 * i1) / 62)
        Dim i3 = src Mod 62
        Dim retval = String.Format("{0}{1}{2}", Encode62Int(i1), Encode62Int(i2), Encode62Int(i3))
        Return retval
    End Function

    Shared Function Decode62Nums(src As String) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim idx As Integer = 0
        Do While src.Length > (idx + 3)
            Dim item = src.Substring(idx, 3)
            Dim element = Decode62Num(item)
            retval.Add(element)
            idx += 3
        Loop
        Return retval
    End Function

    Shared Function Decode62Num(src As String) As Integer
        Dim c1 = Decode62Char(src(0))
        Dim c2 = Decode62Char(src(1))
        Dim c3 = Decode62Char(src(2))
        Dim retval = 62 * 62 * c1 + 62 * c2 + c3
        Return retval
    End Function

    Shared Function Decode62Char(src) As Integer
        Dim retval As Integer
        Dim c = TextHelper.VbAsc(src)
        Select Case c
            Case 48 To 57
                retval = c - 48
            Case 65 To 90
                retval = c - 65 + 10
            Case 97 To 122
                retval = c - 97 + 10 + 26
        End Select
        Return retval
    End Function

    Shared Function Encode62Int(src As Integer) As String
        'encodes a number between 0 and 61 into a string using 10 digits, 26 uppercase and 26 lowercase letters
        Dim retval As String = "0"
        Select Case src
            Case 0 To 9
                retval = TextHelper.VbChr(src + 48)
            Case 10 To 35
                retval = TextHelper.VbChr(src - 10 + 65)
            Case 36 To 61
                retval = TextHelper.VbChr(src - 10 - 26 + 97)
        End Select
        Return retval
    End Function

End Class
