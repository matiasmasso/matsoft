Public Class Frm_Cod

    Private _Cod As DTOCod
    Private _TabLoaded(10) As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Cods
    End Enum

    Public Sub New(value As DTOCod)
        MyBase.New()
        Me.InitializeComponent()
        _Cod = value
    End Sub

    Private Sub Frm_Cod_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Cod.Load(exs, _Cod) Then
            With _Cod
                Xl_TextBoxNumCod.Value = .Id
                TextBoxGuid.Text = .Guid.ToString()
                TextBoxEsp.Text = .Nom.Esp
                TextBoxCat.Text = .Nom.Cat
                TextBoxEng.Text = .Nom.Eng
                TextBoxPor.Text = .Nom.Por
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
         TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
           TextBoxPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Cod
            .Nom.Esp = TextBoxEsp.Text
            .Nom.Cat = TextBoxCat.Text
            .Nom.Eng = TextBoxEng.Text
            .Nom.Por = TextBoxPor.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.Cod.Update(exs, _Cod) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cod))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest codi?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.Cod.Delete(exs, _Cod) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cod))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar el codi")
            End If
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UIHelper.CopyToClipboard(_Cod.Guid.ToString())
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Cods
                If Not _TabLoaded(Tabs.Cods) Then
                    Await refrescaChildren()
                    _TabLoaded(Tabs.Cods) = True
                End If
        End Select
    End Sub

    Private Async Function refrescaChildren() As Task
        Dim exs As New List(Of Exception)
        Dim oChildren = Await FEB.Cods.All(exs, _Cod)
        If exs.Count = 0 Then
            Xl_Cods1.Load(oChildren)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_Cods1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Cods1.RequestToAddNew
        Dim oCod = DTOCod.Factory(Current.Session.User, _Cod)
        Dim oFrm As New Frm_Cod(oCod)
        AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
        oFrm.Show()
    End Sub

    Private Async Sub RefreshRequest(sender As Object, e As MatEventArgs)
        Await refrescaChildren()
    End Sub
End Class


