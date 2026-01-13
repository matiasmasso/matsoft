Public Class DTOCertificatIrpf
    Inherits DTOBaseGuid
    Property filename As String
    Property Nif As String
    Property Nom As String
    Property DocFile As DTODocFile
    Property Contact As DTOContact
    Property Year As Integer
    Property Period As Integer


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub


    Shared Function Factory(sFilename As String) As DTOCertificatIrpf
        Dim retval As New DTOCertificatIrpf
        retval.filename = sFilename
        Return retval
    End Function

    Shared Function Factory(exs As List(Of Exception), year As Integer, filenames As List(Of String)) As List(Of DTOCertificatIrpf)
        Dim retval As New List(Of DTOCertificatIrpf)
        For Each sfilename In filenames
            Dim item = DTOCertificatIrpf.Factory(sfilename)
            Dim src = iTextPdfHelper.readText(sfilename, exs)
            If exs.Count = 0 Then
                Dim lines = src.Split(vbLf).ToList
                Dim keyLine = lines.FirstOrDefault(Function(x) x.Contains("Datos del Perceptor"))
                If keyLine > "" Then
                    Dim idx = lines.IndexOf(keyLine)
                    If lines.Count > idx + 3 Then
                        Dim sLine = lines(idx + 2)
                        Dim iPos = sLine.IndexOf(" ")
                        item.Nif = sLine.Substring(0, iPos)
                        If item.Nif.StartsWith("0") Then item.Nif = item.Nif.Substring(1)
                        item.Nom = sLine.Substring(iPos + 1)
                        item.Year = year
                        retval.Add(item)
                    End If
                End If
            Else
                exs.Add(New Exception("error al llegir '" & sfilename & "'"))
            End If
        Next
        Return retval
    End Function

    Public Function FullPeriod() As String
        Dim retval = _Year.ToString
        If _Period = 0 Then
            retval = String.Format("{0:0000}", _Year)
        Else
            retval = String.Format("{0:0000}-{1:00}", _Year, _Period)
        End If
        Return retval
    End Function
End Class
