Public Class Frm_XecsEnCartera
    Private _Banc As DTOBanc
    Private _AllowEvents As Boolean

    Public Sub New(oBanc As DTOBanc)
        InitializeComponent()
        _Banc = oBanc
    End Sub

    Private Async Sub Frm_XecsEnCartera_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Me.Text = _Banc.Abr & "-Presentacio de efectes en cartera"
        Dim o = Await FEB.Xecs.All(exs, Current.Session.Emp, DTOXec.StatusCods.EnCartera)
        Xl_XecsEnCartera1.DataSource = o
        If exs.Count = 0 Then
            LoadCodisPresentacio()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadCodisPresentacio()
        With ComboBoxCodPresentacio
            .Items.Add(New ListItem(0, "(seleccionar una modalitat)"))
            .Items.Add(New ListItem(DTOXec.ModalitatsPresentacio.A_la_Vista, "ingres a la vista"))
            .Items.Add(New ListItem(DTOXec.ModalitatsPresentacio.Al_Cobro, "presentació al cobro"))
            .Items.Add(New ListItem(DTOXec.ModalitatsPresentacio.Al_Descompte, "presentació al descompte"))
            .SelectedItem = .Items(0)
        End With

    End Sub

    Private Function CurrentXecs() As List(Of DTOXec)
        Dim retval As List(Of DTOXec) = Xl_XecsEnCartera1.SelectedValues
        Return retval
    End Function

    Private Sub Xl_XecsEnCartera1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Xl_XecsEnCartera1.ItemCheck
        Dim oXecs As List(Of DTOXec) = CurrentXecs()
        Xl_AmtCur1.Amt = DTOAmt.Factory(oXecs.Sum(Function(x) x.Amt.Eur))
        EnableButtons()
    End Sub

    Private Sub EnableButtons()
        ButtonOk.Enabled = (CurrentXecs.Count > 0) And (ComboBoxCodPresentacio.SelectedIndex > 0)
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

        If CodPresentacio() = DTOXec.ModalitatsPresentacio.NotSet Then
            MsgBox("cal triar una modalitat de presentació", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf CodPresentacio() = DTOXec.ModalitatsPresentacio.Al_Cobro Then
            MsgBox("Ojo cal repasar com graba Cca" & vbCrLf & "abonem el compte 43100 amb càrrec al 43120 (efectes al cobro) del client")
        Else
        End If

        Dim oXecsPresentacio = DTOXecsPresentacio.Factory(Current.Session.User, DateTimePicker1.Value, _Banc, CodPresentacio)
        oXecsPresentacio.Xecs = CurrentXecs()

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.XecsPresentacio.Update(exs, oXecsPresentacio) Then
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al registrar la presentació")
        End If

    End Sub

    Private Function CodPresentacio() As DTOXec.ModalitatsPresentacio
        Dim retval As DTOXec.ModalitatsPresentacio = DTOXec.ModalitatsPresentacio.NotSet
        Dim oItem As ListItem = ComboBoxCodPresentacio.SelectedItem
        If oItem IsNot Nothing Then
            retval = oItem.Value
        End If
        Return retval
    End Function

    Private Sub ComboBoxCodPresentacio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCodPresentacio.SelectedIndexChanged
        If _AllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Async Sub Xl_XecsEnCartera1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_XecsEnCartera1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oXecs = Await FEB.Xecs.All(exs, Current.Session.Emp, DTOXec.StatusCods.EnCartera)
        If exs.Count = 0 Then
            Xl_XecsEnCartera1.DataSource = oXecs
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Class ListItem
        Private mValue As Integer
        Private mText As String

        Public Sub New(Optional ByVal iValue As Int16 = 0, Optional ByVal sText As String = "")
            mValue = iValue
            mText = sText
        End Sub

        Public Property Value() As Integer
            Get
                Return mValue
            End Get
            Set(ByVal iValue As Integer)
                mValue = iValue
            End Set
        End Property

        Public Property Text() As String
            Get
                Return mText
            End Get
            Set(ByVal value As String)
                mText = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return mText
        End Function
    End Class
End Class