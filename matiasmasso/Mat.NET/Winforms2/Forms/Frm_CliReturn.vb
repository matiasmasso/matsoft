Public Class Frm_CliReturn
    Private _CliReturn As DTOCliReturn
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCliReturn)
        MyBase.New()
        Me.InitializeComponent()
        _CliReturn = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.CliReturn.Load(_CliReturn, exs) Then
            With _CliReturn
                Xl_Contact2Customer.Contact = .Customer
                Xl_Contact2Mgz.Contact = .Mgz
                NumericUpDownBultos.Value = .Bultos
                TextBoxAuth.Text = .Auth
                CheckBoxEntrada.Checked = (.Fch <> Nothing)
                If CheckBoxEntrada.Checked Then
                    GroupBox1.Enabled = True
                    DateTimePicker1.Value = .Fch
                    TextBoxRefMgz.Text = .RefMgz
                    Xl_LookupDelivery1.Delivery = .Entrada
                Else
                    GroupBox1.Enabled = False
                End If
                TextBoxObs.Text = .Obs
                Xl_UsrLog1.Load(.UsrLog)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            DateTimePicker1.ValueChanged,
             Xl_Contact2Customer.AfterUpdate,
              Xl_Contact2Mgz.AfterUpdate,
               NumericUpDownBultos.ValueChanged,
                 TextBoxRefMgz.TextChanged,
                   TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _CliReturn
            .Customer = New DTOCustomer(Xl_Contact2Customer.Contact.Guid)
            .Mgz = New DTOMgz(Xl_Contact2Mgz.Contact.Guid)
            .Auth = TextBoxAuth.Text
            .Bultos = NumericUpDownBultos.Value
            If CheckBoxEntrada.Checked Then
                .Fch = DateTimePicker1.Value
                .RefMgz = TextBoxRefMgz.Text
            End If
            .Entrada = Xl_LookupDelivery1.Delivery
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.CliReturn.Update(_CliReturn, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CliReturn))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.CliReturn.Delete(_CliReturn, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CliReturn))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Function SendRequest(url As String, jsonInputString As String, contentType As String, method As String, ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim uri As New Uri(url)
        Dim jsonDataBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(jsonInputString)
        Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
        req.ContentType = contentType
        req.Method = method
        req.ContentLength = jsonDataBytes.Length

        Try
            Dim stream = req.GetRequestStream()
            If jsonDataBytes IsNot Nothing Then
                stream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
            End If
            stream.Close()

            Dim oResponse As System.Net.WebResponse = req.GetResponse()
            Dim oResponseStream = req.GetResponse().GetResponseStream()

            Dim reader As New System.IO.StreamReader(oResponseStream)
            jsonOutputString = reader.ReadToEnd()
            reader.Close()
            oResponseStream.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Private Sub CheckBoxEntrada_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEntrada.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = CheckBoxEntrada.Checked
            GroupBox1.Enabled = CheckBoxEntrada.Checked
        End If
    End Sub

    Private Async Sub Xl_LookupDelivery1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupDelivery1.RequestToLookup
        Dim exs As New List(Of Exception)
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim oDeliveries = Await FEB.Deliveries.Headers(exs, Current.Session.Emp, contact:=_CliReturn.Customer)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Deliveries(Xl_Deliveries.Purposes.SingleCustomer, oDeliveries, "Albarans de " & _CliReturn.Customer.FullNom, DTO.Defaults.SelectionModes.Selection)
            AddHandler oFrm.onItemSelected, AddressOf onDeliverySelected
            oFrm.Show()
            Cursor = Cursors.Default
            Application.DoEvents()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub onDeliverySelected(sender As Object, e As MatEventArgs)
        Xl_LookupDelivery1.Delivery = e.Argument
        ButtonOk.Enabled = True
    End Sub

End Class


