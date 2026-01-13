Public Class Frm_Incidencia
    Private _Incidencia As DTOIncidencia
    Private _IncidenciaCod As DTOIncidenciaCod
    Private _TancamentCod As DTOIncidenciaCod
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(oIncidencia As DTOIncidencia)
        MyBase.New()
        Me.InitializeComponent()
        _Incidencia = oIncidencia
        _IncidenciaCod = oIncidencia.Codi
        _TancamentCod = oIncidencia.Tancament
        BLL_Incidencia.Load(_Incidencia)
        refresca()
    End Sub

    Private Sub refresca()
        With _Incidencia
            SetCod(.Src)
            If .Id = 0 Then
                Me.Text = "NOVA INCIDENCIA"
            Else
                Me.Text = "INCIDENCIA #" & .Id
                ButtonDel.Enabled = True
                If .Spv IsNot Nothing Then
                    ButtonSpv.Enabled = .Spv.Id = 0
                End If
            End If

            Xl_IncidenciaDocFilesImgs.Load(_Incidencia, Xl_IncidenciaDocFiles.Modes.Images)
            Xl_IncidenciaDocFilesTicket.Load(_Incidencia, Xl_IncidenciaDocFiles.Modes.PurchaseTicket)

            If .Src = DTOIncidencia.Srcs.Producte Then
                Me.Text = Me.Text & " DE PRODUCTE"
                Xl_LookupProduct1.Product = .Product
            Else
                Me.Text = Me.Text & " DE TRANSPORT"
                Xl_LookupProduct1.Visible = False
            End If


            If .Codi Is Nothing Then
                TextBoxCod.Text = "(incidencia pendent de codificar)"
            Else
                _IncidenciaCod = .Codi
                TextBoxCod.Text = .Codi.Esp & " (" & .Codi.Eng & ")"
            End If

            If .Customer Is Nothing Then
                RadioButtonConsumidor.Checked = True
            Else
                RadioButtonProfesional.Checked = True
                Xl_Contact21.Contact = .Customer
            End If

            TextBoxSerialNumber.Text = .SerialNumber
            TextBoxPerson.Text = .ContactPerson
            TextBoxTel.Text = .Tel
            TextBoxEmail.Text = .EmailAdr
            TextBoxRef.Text = .CustomerRef

            ComboBoxProcedencia.SelectedIndex = .Procedencia

            SetRadioButtons()
            DateTimePicker1.Value = .Fch
            TextBoxObs.Text = .Description


            If .FchClose > DateTimePicker2.MinDate Then
                CheckBoxClosed.Checked = True
                DateTimePicker2.Value = .FchClose
                DateTimePicker2.Visible = True
                TextBoxTancament.Visible = True
                ButtonTancamentLookUp.Visible = True

                If .Tancament.Id = 0 Then
                    TextBoxTancament.Text = "(tancament pendent de codificar)"
                Else
                    TextBoxTancament.Text = .Tancament.Esp & " (" & .Tancament.Eng & ")"
                    _TancamentCod = .Tancament
                End If

            Else
            End If

            _AllowEvents = True
        End With
    End Sub

    Private Sub SetCod(ByVal osrc As DTOIncidencia.Srcs)
        Select Case osrc
            Case DTOIncidencia.Srcs.Producte
        End Select
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetRadioButtons()
    End Sub

    Private Sub SetRadioButtons()
        Xl_LookupProduct1.Visible = (_Incidencia.Src = DTOIncidencia.Srcs.Producte)

        Dim BlPro As Boolean = RadioButtonProfesional.Checked
        If BlPro Then
            Xl_Contact21.Visible = True
            Xl_Contact21.Contact = _Incidencia.Customer
        Else
            Xl_Contact21.Clear()
            Xl_Contact21.Visible = False
        End If
    End Sub




    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 TextBoxObs.TextChanged, _
  CheckBoxClosed.CheckedChanged, _
  DateTimePicker1.ValueChanged, _
 DateTimePicker2.ValueChanged, _
  ComboBoxProcedencia.SelectedIndexChanged, _
 TextBoxCod.TextChanged, _
  TextBoxEmail.TextChanged, _
   TextBoxPerson.TextChanged, _
    TextBoxRef.TextChanged, _
     TextBoxSerialNumber.TextChanged, _
      TextBoxTancament.TextChanged, _
       TextBoxTel.TextChanged, _
        Xl_Contact21.AfterUpdate, _
         Xl_IncidenciaDocFilesTicket.AfterUpdate, _
          Xl_IncidenciaDocFilesImgs.AfterUpdate, _
           Xl_LookupProduct1.AfterUpdate

        If _AllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If ReadIncidenciaFromForm() Then

            Dim IsNew As Boolean = (_Incidencia.IsNew)
            Dim exs as New List(Of exception)

            If BLL_Incidencia.Update(_Incidencia, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incidencia))
                If IsNew Then
                    MsgBox("Número de incidencia: " & _Incidencia.Id, MsgBoxStyle.Information, "MAT.NET")
                End If
                Me.Close()
            Else
                MsgBox("error al desar la incidencia" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If

        End If
    End Sub


    Private Sub CheckBoxClosed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxClosed.CheckedChanged
        If _AllowEvents Then
            Dim BlClosed As Boolean = CheckBoxClosed.Checked
            DateTimePicker2.Visible = BlClosed
            TextBoxTancament.Visible = BlClosed
            ButtonTancamentLookUp.Visible = BlClosed
            TextBoxTancament.Text = "(tancament pendent de codificar)"
        End If

    End Sub

    Private Sub ButtonCodLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCodLookUp.Click
        Dim oFrm As New Frm_IncidenciesCods
        AddHandler oFrm.AfterSelect, AddressOf OnCodSelection
        With oFrm
            .Cod = DTOIncidenciaCod.cods.averia
            .Show()
        End With
    End Sub


    Private Sub OnCodSelection(ByVal sender As System.Object, ByVal e As MatEventArgs)
        _IncidenciaCod = CType(e.Argument, DTOIncidenciaCod)
        TextBoxCod.Text = _IncidenciaCod.Esp & " (" & _IncidenciaCod.Eng & ")"
        SetButtons()
    End Sub

    Private Sub OnTancamentSelection(ByVal sender As System.Object, ByVal e As MatEventArgs)
        _TancamentCod = CType(e.Argument, DTOIncidenciaCod)
        TextBoxTancament.Text = _TancamentCod.Esp & " (" & _TancamentCod.Eng & ")"
        SetButtons()
    End Sub

    Private Sub TextBoxCod_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxCod.DoubleClick
        Dim oFrm As New Frm_IncidenciaCod
        AddHandler oFrm.AfterUpdate, AddressOf OnCodSelection
        With oFrm
            .Cod = _IncidenciaCod
            .Show()
        End With

    End Sub


    Private Sub TextBoxTancament_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxTancament.DoubleClick
        Dim oFrm As New Frm_IncidenciaCod
        AddHandler oFrm.AfterUpdate, AddressOf OnTancamentSelection
        With oFrm
            .Cod = _TancamentCod
            .Show()
        End With

    End Sub

    Private Sub SetButtons()
        Dim BlEnable As Boolean = True
        If _IncidenciaCod Is Nothing Then BlEnable = False


        ButtonOk.Enabled = BlEnable
    End Sub


    Private Sub ButtonTancamentLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTancamentLookUp.Click
        Dim oFrm As New Frm_IncidenciesCods
        AddHandler oFrm.AfterSelect, AddressOf OnTancamentSelection
        With oFrm
            .Cod = DTOIncidenciaCod.cods.tancament
            .Show()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        If BLL_Incidencia.Delete(_Incidencia, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            MsgBox("incidencia " & _Incidencia.Id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al eliminar la incidencia")
        End If
    End Sub

    Private Function ReadIncidenciaFromForm() As Boolean
        Dim retval As Boolean
        If Xl_LookupProduct1.Product Is Nothing And _Incidencia.Src = DTOIncidencia.Srcs.Producte Then
            MsgBox("falta posar el producte", MsgBoxStyle.Exclamation, "MAT.NET")
        Else

            With _Incidencia
                .Fch = DateTimePicker1.Value
                .Description = TextBoxObs.Text

                Dim oType As DTOIncidencia.ContactTypes = IIf(RadioButtonProfesional.Checked, DTOIncidencia.ContactTypes.Professional, DTOIncidencia.ContactTypes.Consumidor)
                .ContactType = oType
                If oType = DTOIncidencia.ContactTypes.Professional Then
                    If Xl_Contact21.Contact Is Nothing Then
                        .Customer = Nothing
                    Else
                        .Customer = New DTOCustomer(Xl_Contact21.Contact.Guid)
                        .Customer.FullNom = Xl_Contact21.Contact.FullNom
                    End If
                End If

                .Codi = _IncidenciaCod
                .Tancament = _TancamentCod

                .Product = Xl_LookupProduct1.Product

                .SerialNumber = TextBoxSerialNumber.Text
                .ContactPerson = TextBoxPerson.Text
                .Tel = TextBoxTel.Text
                .EmailAdr = TextBoxEmail.Text
                .CustomerRef = TextBoxRef.Text

                .Procedencia = ComboBoxProcedencia.SelectedIndex

                .Description = TextBoxObs.Text
                If CheckBoxClosed.Checked Then
                    .FchClose = DateTimePicker2.Value
                Else
                    .FchClose = DateTime.MinValue
                End If

                .DocFileImages = Xl_IncidenciaDocFilesImgs.Values
                .PurchaseTickets = Xl_IncidenciaDocFilesTicket.Values

            End With
            retval = True
        End If
        Return retval
    End Function


    Private Sub ButtonSpv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSpv.Click
        Dim iCODREPARACIO As Integer = 34

        If ReadIncidenciaFromForm() Then
            _Incidencia.FchClose = Today
            _Incidencia.Tancament = New DTOIncidenciaCod(iCODREPARACIO)
            Dim IsNew As Boolean = _Incidencia.IsNew
            Dim exs as New List(Of exception)

            If BLL_Incidencia.Update(_Incidencia, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incidencia))
                If IsNew Then
                    MsgBox("Número de incidencia: " & _Incidencia.Id, MsgBoxStyle.Information, "MAT.NET")
                End If

                Dim oFrm As New Frm_Spv(_Incidencia)
                oFrm.Show()
                Me.Close()
            Else
                MsgBox("error al desar la incidencia" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If


        End If
    End Sub

    Private Sub ButtonNewPdc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNewPdc.Click
        If _TancamentCod Is Nothing Then
            MsgBox("falta posar el codi de tancament de la incidencia", MsgBoxStyle.Exclamation, "MAT.NET")
        ElseIf ReadIncidenciaFromForm() Then
            Select Case _TancamentCod.Id
                Case 31, 32, 62
                    With _Incidencia
                        .FchClose = Today
                    End With

                    Dim IsNew As Boolean = (_Incidencia.IsNew)
                    Dim exs as New List(Of exception)

                    If BLL_Incidencia.Update(_Incidencia, exs) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incidencia))
                        If IsNew Then
                            MsgBox("Número de incidencia: " & _Incidencia.Id, MsgBoxStyle.Information, "MAT.NET")
                        End If

                        Dim oCustomer As DTOCustomer = BLL.BLLCustomer.Find(Xl_Contact21.Contact.Guid)
                        BLL.BLLContact.Load(oCustomer)
                        Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.NewCustomerOrder(oCustomer, Today)
                        With oPurchaseOrder
                            .Concept = "incidencia " & _Incidencia.Id
                            .Source = DTOPurchaseOrder.Sources.no_Especificado
                        End With
                        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                            UIHelper.WarnError(exs)
                        Else
                            Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                            oFrm.Show()
                            Me.Close()
                        End If
                    Else
                        MsgBox("error al desar la incidencia" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If

                Case Else
                    MsgBox("El codi de tancament de la incidencia no sembla l'adequat per resoldre-ho amb una comanda", MsgBoxStyle.Exclamation, "MAT.NET")
            End Select
        End If
    End Sub


    Private Sub ButtonCancel_Click1(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class