Imports System.Xml.Serialization
Imports System.IO
Public Class jLegido

    Shared Function procede(oInvoices As List(Of DTOInvoice), cert As Security.Cryptography.X509Certificates.X509Certificate2, ByRef resp As SoapFacturasEmitidas.RespuestaLRFEmitidasType, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim Ws As SoapFacturasEmitidas.siiSOAPClient = CurrentWebService(cert)
        Dim oSuministroLRFacturasEmitidas As SoapFacturasEmitidas.SuministroLRFacturasEmitidas = FacturasEmitidas.FromInvoices(oInvoices)

        'Opcional: per debug crea un fitxer amb el resultat
        Dim sfilename As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\_tmp\serialXML.xml"
        Serialize(oSuministroLRFacturasEmitidas, sfilename, exs)
        '--------------------------------------------------

        Try
            resp = Ws.SuministroLRFacturasEmitidas(oSuministroLRFacturasEmitidas)
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Shared Function Serialize(o As Object, sfilename As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim serialWriter As StreamWriter
        Dim xmlWriter As XmlSerializer
        Try
            serialWriter = New StreamWriter(sfilename)
            xmlWriter = New XmlSerializer(o.GetType())
            xmlWriter.Serialize(serialWriter, o)
            serialWriter.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function CurrentWebService(oCert As Security.Cryptography.X509Certificates.X509Certificate2) As SoapFacturasEmitidas.siiSOAPClient
        Dim retval As New SoapFacturasEmitidas.siiSOAPClient
        With retval
            .ClientCredentials.ClientCertificate.Certificate = oCert
            .ClientCredentials.UseIdentityConfiguration = True
        End With
        Return retval
    End Function

End Class
