Public Class Frm_PrNumero

    Private _Numero As PrNumero
    Private _DirtyEditorial As PrEditorial
    Private _DirtyFchs As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Enum Tabs
        General
        Insercions
    End Enum

    Public Sub New(ByVal oNumero As PrNumero)
        MyBase.New()
        Me.InitializeComponent()
        LoadMesos()
        _Numero = oNumero
        If _Numero.IsNew Then
            Me.Text = "(NOU NUMERO DE " & _Numero.Revista.Nom & ")"
        Else
            Me.Text = "Numero: " & _Numero.Numero & " DE " & _Numero.Revista.Nom
            ButtonDel.Enabled = True
        End If
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub LoadMesos()
        Dim oLang As DTOLang = BLL.BLLApp.Lang
        For i As Integer = 1 To 12
            DomainUpDownMes.Items.Add(oLang.MesAbr(i))
        Next
    End Sub

    Private Sub Refresca()
        With _Numero
            TextBoxNumero.Text = .Numero
            NumericUpDownYea.Value = .Yea
            DomainUpDownMes.SelectedIndex = (12 - .Mes)
            If .FchDeadline > Date.MinValue Then
                DateTimePickerDeadline.Value = .FchDeadline
            End If
            If .FchOut > Date.MinValue Then
                DateTimePickerFchOut.Value = .FchOut
            End If
            PictureBox1.Image = .Revista.Logo
            Xl_DocFile1.Load(.DocFile)
        End With
    End Sub

    Private Sub DateTimePickerDesde_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerFchOut.Validated
        If _AllowEvents Then
            _DirtyFchs = True
        End If
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBoxNumero.TextChanged, _
        DateTimePickerFchOut.ValueChanged, _
         DateTimePickerDeadline.ValueChanged, _
          Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub


    Private Sub YYMM_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     NumericUpDownYea.ValueChanged, _
      DomainUpDownMes.SelectedItemChanged

        If _AllowEvents Then
            If Not _DirtyFchs Then
                Dim iYea As Integer = NumericUpDownYea.Value
                Dim iMes As Integer = (12 - DomainUpDownMes.SelectedIndex)
                Dim iDaysinMonth As Integer = Date.DaysInMonth(iYea, iMes)
                DateTimePickerFchOut.Value = New Date(iYea, iMes, 1)
                DateTimePickerDeadline.Value = New Date(iYea, iMes, iDaysinMonth)
            End If
            ButtonOk.Enabled = True
        End If

    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Save()
        Me.Close()
    End Sub

    Private Sub Save()
        With _Numero
            .Numero = TextBoxNumero.Text
            .Yea = NumericUpDownYea.Value
            .Mes = (12 - DomainUpDownMes.SelectedIndex)
            .FchDeadline = DateTimePickerDeadline.Value
            .FchOut = DateTimePickerFchOut.Value
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If
        End With

        Dim exs as New List(Of exception)
        If PrNumeroLoader.Update(_Numero, exs) Then
            ButtonOk.Enabled = False
            RaiseEvent AfterUpdate(_Numero, New System.EventArgs)
        Else
            UIHelper.WarnError( exs, "error al registrar el número")
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Insercions
                Static BlDone As Boolean
                If Not BlDone Then
                    LoadInsercions()
                    BlDone = True
                End If
        End Select
    End Sub

    Private Sub LoadInsercions()
        Dim oPrInsercions As PrInsercions = PrInsercioLoader.FromNumero(_Numero)
        Xl_PrInsercions1.Load(oPrInsercions)
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        If PrNumeroLoader.Delete(_Numero, exs) Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al eliminar el número")
        End If
    End Sub

    Private Sub Xl_PrInsercions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PrInsercions1.RequestToAddNew
        Dim oPrInsercio As New PrInsercio(_Numero)
        If _Numero.IsNew Then
            Dim exs as New List(Of exception)
            If Not PrNumeroLoader.Update(_Numero, exs) Then
                UIHelper.WarnError( exs, "error al desar el número")
                Exit Sub
            End If
        End If

        Dim oFrm As New Frm_PrInsercio(oPrInsercio)
        AddHandler oFrm.AfterUpdate, AddressOf LoadInsercions
        oFrm.Show()
    End Sub

    Private Sub Xl_PrInsercions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PrInsercions1.RequestToRefresh
        LoadInsercions()
    End Sub
End Class