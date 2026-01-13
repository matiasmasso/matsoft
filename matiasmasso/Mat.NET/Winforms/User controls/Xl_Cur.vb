Public Class Xl_Cur
    Inherits TextBox

    Private _Cur As DTOCur

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New()
        MyBase.New
        MyBase.Size = New Size(30, 20)
        MyBase.ReadOnly = True
        MyBase.TabStop = False
        MyBase.Cursor = Cursors.Hand
    End Sub

    Public Property Cur As DTOCur
        Get
            Return _Cur
        End Get
        Set(value As DTOCur)
            _Cur = value
            If value Is Nothing Then
                MyBase.Clear()
            Else
                MyBase.Text = value.Tag
            End If
        End Set
    End Property

    Public WriteOnly Property Locked() As Boolean
        Set(ByVal Value As Boolean)
            MyBase.Enabled = Not Value
            If Value Then
                MyBase.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control)
            Else
                MyBase.BackColor = System.Drawing.Color.White
            End If
        End Set
    End Property

    Private Shadows Sub onClick() Handles MyBase.Click
        Dim oFrm As New Frm_Curs(_Cur, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onUpdateRequest
        oFrm.Show()
    End Sub

    Private Sub onUpdateRequest(sender As Object, e As MatEventArgs)
        _Cur = e.Argument
        MyBase.Text = _Cur.Tag
        RaiseEvent AfterUpdate(Me, e)
    End Sub


End Class
