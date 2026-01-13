Imports System.Xml

Public Class Transmisio
    Inherits _FeblBase

#Region "CRUD"
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOTransmisio)
        Return Await Api.Fetch(Of DTOTransmisio)(exs, "transmisio", oGuid.ToString())
    End Function

    Shared Async Function FromNum(oEmp As DTOEmp, iYea As Integer, iId As Integer, exs As List(Of Exception)) As Task(Of DTOTransmisio)
        Return Await Api.Fetch(Of DTOTransmisio)(exs, "transmisio/FromNum", oEmp.Id, iYea, iId)
    End Function

    Shared Function Load(ByRef oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Boolean
        If Not oTransmisio.IsLoaded And Not oTransmisio.IsNew Then
            Dim pTransmisio = Api.FetchSync(Of DTOTransmisio)(exs, "Transmisio", oTransmisio.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTransmisio)(pTransmisio, oTransmisio, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTransmisio)(oTransmisio, exs, "Transmisio")
    End Function

    Shared Async Function Delete(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTransmisio)(oTransmisio, exs, "Transmisio")
    End Function

    Shared Async Function Excel(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Task(Of MatHelper.Excel.Book)
        Return Await Api.Fetch(Of MatHelper.Excel.Book)(exs, "transmisio/Excel", oTransmisio.Guid.ToString())
    End Function

#End Region


    Shared Async Function Send(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oDeliveries As List(Of DTODelivery) = Await Deliveries.PendentsDeTransmetre(oEmp.Mgz, exs)
        If exs.Count = 0 AndAlso oDeliveries.Count > 0 Then
            Dim oTransmisio = DTOTransmisio.Factory(oEmp, oEmp.Mgz, oDeliveries)
            retval = Await Transmisio.Update(oTransmisio, exs)
            If exs.Count = 0 Then
                retval = Await Transmisio.Send(exs, oTransmisio)
            End If
        End If
        Return retval
    End Function

    Shared Async Function Send(exs As List(Of Exception), oTransmisio As DTOTransmisio, Optional sTo As String = "") As Task(Of Boolean)
        Return Await Api.Execute(Of String, Boolean)(sTo, exs, "transmisio/send", oTransmisio.Guid.ToString())
    End Function

    Shared Async Function NextTime(exs As List(Of Exception)) As Task(Of DateTimeOffset)
        Dim retval As DateTimeOffset
        Dim pTask = DTOTask.Wellknown(DTOTask.Cods.VivaceTransmisio)
        Dim oTask = Await Task.Find(pTask.Guid, exs)
        If exs.Count = 0 Then
            retval = oTask.NextRun()
        End If
        Return retval
    End Function


    Shared Async Function XmlAttachment(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Task(Of DTOMailMessage.Attachment)
        Dim retval As DTOMailMessage.Attachment = Nothing

        Dim oByteArray = Await Transmisio.XmlFile(oTransmisio, exs)
        If exs.Count = 0 Then
            Dim text As String = System.Text.ASCIIEncoding.UTF8.GetString(oByteArray)
            Dim sTempPath = MatHelperStd.FileSystemHelper.TmpFolder() ' My.Computer.FileSystem.SpecialDirectories.Temp
            Dim sFilename = String.Format("{0}\{1}", sTempPath, Transmisio.FileNameDades(oTransmisio))

            Dim objWriter As New System.IO.StreamWriter(sFilename, append:=False)
            objWriter.Write(text)
            objWriter.Close()

            retval = DTOMailMessage.Attachment.Factory(sFilename, sFilename)
        End If

        Return retval
    End Function



    Shared Function SendUrlBody(oTransmisio As DTOTransmisio) As String
        Dim retval As String = UrlHelper.Factory(True, "mail", "transmisio", oTransmisio.Guid.ToString())
        Return retval
    End Function

    'Shared Function UrlAlbarans(oTransmisio As DTOTransmisio) As String
    '    Dim retval = UrlHelper.Factory(True, "doc", CInt(DTODocFile.Cods.TransmisioAlbarans), oTransmisio.Guid.ToString())
    '    Return retval
    'End Function

    Shared Async Function SendTo(exs As List(Of Exception)) As Task(Of String)
        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        Dim retval As String = Await Emp.GetEmpValue(oEmp, DTODefault.Codis.EmailTransmisioVivace, exs)
        Return retval
    End Function

    Shared Function SendSubject(oTransmisio As DTOTransmisio) As String
        Return "Transmisió num." & oTransmisio.id.ToString
    End Function


    Shared Async Function XmlFile(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchBinary(exs, "transmisio/XmlFile", oTransmisio.Guid.ToString())
    End Function

    Shared Async Function PdfDeliveries(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchBinary(exs, UrlPdfDeliveries(oTransmisio))
        'Return Await Api.FetchBinary(exs, "transmisio/PdfDeliveries", oTransmisio.Guid.ToString())
    End Function

    Shared Function UrlPdfDeliveries(oTransmisio As DTOTransmisio) As String
        Dim retval = String.Format("https://api2.matiasmasso.es/transmission/deliveries/pdf/{0}", oTransmisio.Guid.ToString())
        Return retval
        ' Return Api.ApiUrl("transmisio/PdfDeliveries", oTransmisio.Guid.ToString())
    End Function


    Shared Function FileNameDades(oTransmisio As DTOTransmisio) As String
        Return String.Format("{0}.dades.xml", FilePrefix(oTransmisio))
    End Function

    Shared Function FileNameDocs(oTransmisio As DTOTransmisio) As String
        Dim retval = String.Format("{0}.documentacio.pdf", FilePrefix(oTransmisio))
        Return retval
    End Function

    Shared Function FilePrefix(oTransmisio As DTOTransmisio) As String
        Return "M+O.transmisio." & oTransmisio.fch.Year.ToString & "." & TextHelper.VbFormat(oTransmisio.id, "0000")
    End Function


    Private Shared Function AddNode(oParent As XmlElement, Nom As String, Value As String) As XmlElement
        Dim DOM = oParent.OwnerDocument
        Dim retval As XmlElement = DOM.CreateElement(Nom)
        retval.Value = Value
        oParent.AppendChild(retval)
        Return retval
    End Function

End Class

Public Class Transmisions
    Inherits _FeblBase
    Shared Async Function All(oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of List(Of DTOTransmisio))
        Return Await Api.Fetch(Of List(Of DTOTransmisio))(exs, "transmisions", oMgz.Guid.ToString())
    End Function

    Shared Async Function HoldingHeaders(exs As List(Of Exception), oHolding As DTOHolding, daysFrom As Integer) As Task(Of List(Of DTOTransmisio))
        Return Await Api.Fetch(Of List(Of DTOTransmisio))(exs, "transmisions/HoldingHeaders", oHolding.Guid.ToString(), daysFrom)
    End Function

    Shared Async Function Orders(exs As List(Of Exception), oTransmGuids As List(Of Guid)) As Task(Of List(Of DTOPurchaseOrder))
        Return Await Api.Execute(Of List(Of Guid), List(Of DTOPurchaseOrder))(oTransmGuids, exs, "transmisions/Orders")
    End Function


End Class