Public Class Xl_Amount
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(78, 20)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_Amt
        '
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Xl_Amt"
        Me.Size = New System.Drawing.Size(78, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Changed()

    Private mAmt As MaxiSrvr.Amt
    Private mCurrentVal As Decimal
    Private mAllowEvents As Decimal
    Private mSwitchPeriodToComma As Boolean
    Private mPeriodToCommaPos As Integer


    Public Property Amt() As MaxiSrvr.Amt
        Get
            Dim oAmt As MaxiSrvr.Amt = Nothing
            If mAmt IsNot Nothing Then
                oAmt = mAmt.Clone
            End If
            Return oAmt
        End Get
        Set(ByVal value As MaxiSrvr.Amt)
            mAmt = value
            mAllowEvents = False
            If Not mAmt Is Nothing Then Display()
            mAllowEvents = True
        End Set
    End Property

    Public Property Amt2() As DTOAmt
        Get
            Dim oAmt As DTOAmt = Nothing
            If mAmt IsNot Nothing Then
                Dim oCur As DTOCur = DTOCur.FromTag(mAmt.Cur.Id.ToString)
                oAmt = New DTOAmt(mAmt.Eur, oCur, mAmt.Val)
            End If
            Return oAmt
        End Get
        Set(ByVal value As DTOAmt)
            If value IsNot Nothing Then
                Dim oCur As New Cur
                oCur.Id = value.Cur.Id.ToString
                mAmt = New Amt(value.Eur, oCur, value.Val)
                mAllowEvents = False
                If Not mAmt Is Nothing Then Display()
                mAllowEvents = True
            End If
        End Set
    End Property

    Public Shadows ReadOnly Property Text() As String
        Get
            Return TextBox1.Text
        End Get
    End Property

    Public WriteOnly Property Locked() As Boolean
        Set(ByVal Value As Boolean)
            TextBox1.ReadOnly = Value
            If Value Then
                TextBox1.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control)
            Else
                TextBox1.BackColor = System.Drawing.Color.White
            End If
        End Set
    End Property

    Private Sub Display()
        TextBox1.Text = mAmt.Formatted
    End Sub

    Private Sub Xl_Amt_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        TextBox1.Width = MyBase.Width
        MyBase.Height = TextBox1.Height
    End Sub

    Private Sub TextBox1_Validated(sender As Object, e As EventArgs) Handles TextBox1.Validated
        If Me.Amt IsNot Nothing Then
            TextBox1.Text = Me.Amt.Formatted
        End If
    End Sub

    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If mAllowEvents Then
            Dim s As String = TextBox1.Text

            'Dim iComaDecimalPos As Integer = s.LastIndexOfAny(".")
            'If iComaDecimalPos >= s.Length - 3 And iComaDecimalPos < s.Length Then
            'se asume coma decimal si tiene menos de tres digitos
            's = s.Substring(0, iComaDecimalPos) & "," & s.Substring(iComaDecimalPos + 1)
            'End If
            s = s.Replace(".", "")

            If s = "" Then
                If Not mAmt Is Nothing Then
                    If Not mAmt.Cur Is Nothing Then
                        'If mAmt.Val <> 0 Then
                        Me.Amt = New MaxiSrvr.Amt()
                        RaiseEvent AfterUpdate(Amt, EventArgs.Empty)
                        'End If
                    End If
                End If
            Else
                If IsNumeric(s) Then
                    Dim oCur As MaxiSrvr.Cur = mAmt.Cur
                    Dim DecVal As Decimal = CDec(s)
                    Dim DecEur As Decimal = Math.Round(DecVal * oCur.Euros, 2)
                    'Me.Amt = New maxisrvr.Amt(DecEur, oCur, DecVal)
                    mAmt = New MaxiSrvr.Amt(DecEur, oCur, DecVal)
                    Dim oClone As Amt = mAmt.Clone
                    RaiseEvent AfterUpdate(oClone, EventArgs.Empty)
                Else
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub Opera(ByRef s As String)

        Dim iPos As Integer = s.IndexOf("+")
        If iPos = -1 Then iPos = s.IndexOf("-")
        If iPos = -1 Then iPos = s.IndexOf("*")
        If iPos = -1 Then iPos = s.IndexOf("/")

        Dim FirstNum As Decimal = CDbl(s.Substring(0, iPos))
        Dim LastNum As Decimal = CDbl(s.Substring(iPos + 1))
        Dim Operation As String = s.Substring(iPos, 1)
        Select Case Operation
            Case "+"
                s = FirstNum + LastNum
            Case "-"
                s = FirstNum - LastNum
            Case "*"
                s = FirstNum * LastNum
            Case "/"
                s = FirstNum / LastNum
        End Select
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim CancelEvent As Boolean
        If Char.IsLetter(e.KeyChar) Then CancelEvent = True
        If Char.IsPunctuation(e.KeyChar) Then CancelEvent = True
        Select Case e.KeyChar
            Case "."
                CancelEvent = False
                mSwitchPeriodToComma = True
                mPeriodToCommaPos = TextBox1.SelectionStart
            Case ",", "-", "+", "*", "/"
                CancelEvent = False
        End Select
        e.Handled() = CancelEvent
    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim s As String
        If mSwitchPeriodToComma Then
            mSwitchPeriodToComma = False
            s = TextBox1.Text.Substring(0, mPeriodToCommaPos) & ","
            If TextBox1.Text.Length > s.Length Then s = s & TextBox1.Text.Substring(mPeriodToCommaPos + 1)
            TextBox1.Text = s
            TextBox1.SelectionStart = mPeriodToCommaPos + 1
            TextBox1.SelectionLength = 0
        End If

        s = TextBox1.Text
        If s = "" Then s = "0"
        If IsNumeric(s) Then

            Dim oCur As MaxiSrvr.Cur = Nothing
            If mAmt Is Nothing Then
            Else
                oCur = mAmt.Cur
            End If
            If oCur Is Nothing Then oCur = MaxiSrvr.Cur.Eur
            Dim DecVal As Decimal = CDec(s)
            Dim DecEur As Decimal = Math.Round(DecEur * oCur.Euros, 2)
            mAmt = New MaxiSrvr.Amt(DecEur, oCur, DecVal)
        End If

        RaiseEvent Changed()
    End Sub

    Public Overrides Property BackColor() As System.Drawing.Color
        Get
            Return TextBox1.BackColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            TextBox1.BackColor = Value
        End Set
    End Property
End Class
