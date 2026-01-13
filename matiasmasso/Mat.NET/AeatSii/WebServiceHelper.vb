Imports System.IO
Imports System.Xml.Serialization
Module WebServiceHelper



    Public Sub Serialize(o As Object, Optional sFilename As String = "")
        'Opcional: per debug crea un fitxer amb el resultat
        If sFilename = "" Then
            sFilename = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\_tmp\serialXML.xml"
        End If
        Dim exs As New List(Of Exception)
        Serialize(o, sFilename, exs)
    End Sub

    Public Function Serialize(o As Object, sFilename As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim serialWriter As StreamWriter
        Dim xmlWriter As XmlSerializer
        Try
            serialWriter = New StreamWriter(sFilename)
            xmlWriter = New XmlSerializer(o.GetType())
            xmlWriter.Serialize(serialWriter, o)
            serialWriter.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Function FormatNumber(oAmt As DTOAmt) As String
        Dim retval As String = ""
        If oAmt IsNot Nothing Then
            retval = oAmt.Eur.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
        End If
        Return retval
    End Function

    Public Function FormatNumber(DcNumber As Decimal) As String
        Dim retval As String = DcNumber.ToString("#0.00", System.Globalization.CultureInfo.InvariantCulture)
        Return retval
    End Function

    Public Function FormatFch(DtFch As Date) As String
        Dim retval As String = Format(DtFch, "dd-MM-yyyy")
        Return retval
    End Function

End Module
