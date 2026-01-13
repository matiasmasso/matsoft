Public Class Xl_Iban
    Private _Iban As DTOIban
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToChange(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToAddNew(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Private _Titular As DTOContact
    Private _Cod As DTOIban.Cods

    Public Shadows Async Function Load(oIban As DTOIban, Optional oTitular As DTOContact = Nothing, Optional oCod As DTOIban.Cods = DTOIban.Cods._NotSet) As Task
        Dim exs As New List(Of Exception)
        _Iban = oIban
        _Titular = oTitular
        _Cod = oCod
        If _Iban Is Nothing Then
            LabelNoDigits.Visible = True
        Else
            If FEB.Iban.Load(_Iban, exs) Then
                LabelNoDigits.Visible = False
                Await Refresca()
                SetContextMenu()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Public ReadOnly Property Value As DTOIban
        Get
            Return _Iban
        End Get
    End Property

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        If _Iban Is Nothing Then
            LabelNoDigits.Visible = True
        ElseIf _Iban.Digits = "" Then
            LabelNoDigits.Visible = True
        Else
            Dim oImgBytes = Await FEB.Iban.Img(exs, _Iban, Current.Session.Lang)
            PictureBox1.Image = LegacyHelper.ImageHelper.FromBytes(oImgBytes)
            If exs.Count > 0 Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        If _Iban IsNot Nothing Then
            Dim oMenu_Iban As New Menu_Iban(_Iban)
            AddHandler oMenu_Iban.AfterUpdate, AddressOf Refresca
            oContextMenu.Items.AddRange(oMenu_Iban.Range)
        End If
        oContextMenu.Items.Add("-")
        If _Iban Is Nothing Then
            oContextMenu.Items.Add("entrar digits", Nothing, AddressOf Do_AddNew)
        Else
            oContextMenu.Items.Add("canviar", Nothing, AddressOf Do_Change)
        End If

        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Change()
        RaiseEvent RequestToChange(Me, New MatEventArgs(_Iban))
        'Dim oContact As DTOContact = _Iban.Titular
        'Dim oFrm As New Frm_Contact_Ibans(oContact)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles _
        PictureBox1.DoubleClick, LabelNoDigits.DoubleClick
        If _Iban Is Nothing Then
            Do_AddNew()
        Else
            Select Case _Iban.Cod
                Case DTOIban.Cods.Client
                    Dim oFrm As New Frm_CustomerIbans(_Iban.Titular)
                    oFrm.Show()
                Case Else
                    If _Iban Is Nothing Then
                        _Iban = DTOIban.Factory(GlobalVariables.Emp, _Titular, _Cod)
                    End If
                    Dim oFrm As New Frm_IbanCcc(_Iban)
                    AddHandler oFrm.AfterUpdate, AddressOf Refresca
                    oFrm.Show()
            End Select
        End If
    End Sub

    Private Async Sub Refresca(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        _Iban.IsLoaded = False
        If FEB.Iban.Load(_Iban, exs) Then
            Await Refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub SelleccionarForaDeLaUEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelleccionarForaDeLaUEToolStripMenuItem.Click
        Dim oFrm As New Frm_Ccc(_Iban)
        AddHandler oFrm.AfterUpdate, AddressOf afterSelectFromOutsideUE
        oFrm.show
    End Sub

    Private Async Sub afterSelectFromOutsideUE(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        _Iban = e.Argument
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Iban))
        Await Refresca()
    End Sub

End Class
