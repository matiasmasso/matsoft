Public Class Frm_CodiMercancia
    Private _CodiMercancia As DTOCodiMercancia
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCodiMercancia)
        MyBase.New()
        Me.InitializeComponent()
        _CodiMercancia = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _CodiMercancia.id = "" Then
            _AllowEvents = True
        Else
            If FEB.CodiMercancia.Load(_CodiMercancia, exs) Then
                With _CodiMercancia
                    TextBoxCodi.Text = .id
                    TextBoxDsc.Text = .dsc
                    Dim oProducts = Await FEB.CodiMercancia.Products(_CodiMercancia, exs)
                    If exs.Count = 0 Then
                        Xl_Products1.Load(oProducts)
                        ButtonDel.Enabled = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End With
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxCodi.TextChanged,
         TextBoxDsc.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        With _CodiMercancia
            .Id = TextBoxCodi.Text
            .Dsc = TextBoxDsc.Text
        End With

        If Await FEB.CodiMercancia.Update(_CodiMercancia, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CodiMercancia))
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
        Dim rc As MsgBoxResult = MsgBox("eliminem el codi '" & _CodiMercancia.Id & "'?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.CodiMercancia.Delete(_CodiMercancia, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CodiMercancia))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


