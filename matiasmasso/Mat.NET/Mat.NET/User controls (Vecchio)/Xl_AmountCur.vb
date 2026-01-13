Public Class Xl_AmountCur
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
    Friend WithEvents Xl_Amt1 As Xl_Amount
    Friend WithEvents Xl_Cur1 As Xl_Cur
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Xl_Amt1 = New Xl_Amount
        Me.Xl_Cur1 = New Xl_Cur
        Me.SuspendLayout()
        '
        'Xl_Amt1
        '
        Me.Xl_Amt1.Amt = Nothing
        Me.Xl_Amt1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Amt1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Amt1.Name = "Xl_Amt1"
        Me.Xl_Amt1.Size = New System.Drawing.Size(116, 20)
        Me.Xl_Amt1.TabIndex = 0
        '
        'Xl_Cur1
        '
        Me.Xl_Cur1.Cur = Nothing
        Me.Xl_Cur1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Xl_Cur1.Location = New System.Drawing.Point(120, 0)
        Me.Xl_Cur1.Name = "Xl_Cur1"
        Me.Xl_Cur1.Size = New System.Drawing.Size(30, 20)
        Me.Xl_Cur1.TabIndex = 1
        Me.Xl_Cur1.TabStop = False
        '
        'Xl_AmtCur
        '
        Me.Controls.Add(Me.Xl_Cur1)
        Me.Controls.Add(Me.Xl_Amt1)
        Me.Name = "Xl_AmtCur"
        Me.Size = New System.Drawing.Size(150, 20)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdateValue(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdateCur(ByVal oCur As MaxiSrvr.Cur)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Amt() As MaxiSrvr.Amt
        Get
            Return Xl_Amt1.Amt
        End Get
        Set(ByVal Value As MaxiSrvr.Amt)
            If Not Value Is Nothing Then
                Xl_Amt1.Amt = Value
                Xl_Cur1.Cur = Value.Cur
            End If
        End Set
    End Property


    Public Property Amt2() As DTOAmt
        Get
            Dim oAmt As DTOAmt = Nothing
            If Xl_Amt1.Amt IsNot Nothing Then
                Dim oCur As DTOCur = DTOCur.FromTag(Xl_Amt1.Amt.Cur.Id.ToString)
                oAmt = New DTOAmt(Xl_Amt1.Amt.Eur, oCur, Xl_Amt1.Amt.Val)
            End If
            Return oAmt
        End Get
        Set(ByVal value As DTOAmt)
            If Not value Is Nothing Then
                Dim pCur As New Cur
                pCur.Id = value.Cur.Id.ToString
                Xl_Amt1.Amt2 = value
                Xl_Cur1.Cur = pCur
            End If
        End Set
    End Property

    Public Shadows ReadOnly Property Text() As String
        Get
            Return Xl_Amt1.Text
        End Get
    End Property

    Public WriteOnly Property Locked() As Boolean
        Set(ByVal Value As Boolean)
            Xl_Amt1.locked = Value
            Xl_Cur1.Locked = Value
        End Set
    End Property

    Private Sub Xl_AmtCur_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim oXl As Windows.Forms.Control = Xl_Cur1
        Xl_Amt1.Width = MyBase.Width - Xl_Cur1.Width
        Xl_Cur1.Left = Xl_Amt1.Width
        MyBase.Height = oXl.Top + oXl.Height
    End Sub

    Private Sub Xl_Amt1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Amt1.AfterUpdate
        RaiseEvent AfterUpdateValue(Me.Amt, New System.EventArgs)
        RaiseEvent AfterUpdate(Me, New System.EventArgs)
    End Sub

    Private Sub Xl_Cur1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Cur1.AfterUpdate
        Dim oAmt As MaxiSrvr.Amt
        If Xl_Amt1.Amt Is Nothing Then
            oAmt = New MaxiSrvr.Amt(0, Xl_Cur1.Cur, 0)
        Else
            oAmt = New MaxiSrvr.Amt(Xl_Amt1.Amt.Eur, Xl_Cur1.Cur, Xl_Amt1.Amt.Val)
        End If

        Xl_Amt1.Amt = oAmt
        RaiseEvent AfterUpdateCur(oAmt.Cur)
        RaiseEvent AfterUpdate(Me, New System.EventArgs)
    End Sub

    Private Sub Xl_Amt1_Changed() Handles Xl_Amt1.Changed
        RaiseEvent Changed(Me, New System.EventArgs)
    End Sub
End Class
