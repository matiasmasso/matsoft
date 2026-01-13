Public Class Menu_BancTransferItem
    Inherits Menu_Base

    Property BancTransferItem As DTOBancTransferItem


    Shared Function Factory(ByVal oBancTransferItem As DTOBancTransferItem) As Menu_BancTransferItem
        Dim retval As New Menu_BancTransferItem
        retval.BancTransferItem = oBancTransferItem
        retval.AddMenuItem(MenuItem_Zoom(oBancTransferItem))
        retval.AddMenuItem(MenuItem_Delete(oBancTransferItem))
        Return retval
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Shared Function MenuItem_Zoom(ByVal oBancTransferItem As DTOBancTransferItem) As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Beneficiari"
        Dim oMenu_Contact As New Menu_Contact(oBancTransferItem.Contact)
        oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
        Return oMenuItem
    End Function

    Shared Function MenuItem_Delete(ByVal oBancTransferItem As DTOBancTransferItem) As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compte"
        Dim oMenu_Iban As New Menu_Iban(oBancTransferItem.Iban)
        oMenuItem.DropDownItems.AddRange(oMenu_Iban.Range)
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================




End Class


