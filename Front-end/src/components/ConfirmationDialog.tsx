import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button } from "@material-ui/core";

interface Props {
  open: boolean;
  onClose: () => void;
  onConfirm: () => void;
  title: string;
  description: string;
}

const ConfirmationDialog: React.FC<Props> = (props: Props) =>{
  const {open, onClose, onConfirm, title, description} = props;
  return (
    <Dialog
          open={open}
          onClose={() => onClose()}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
          className="cancel-popup"
        >
          <DialogTitle id="alert-dialog-title">{title}</DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              {description}
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button
              type="button"
              onClick={() => onClose()}
              className="btn pink-btn"
            >
              Cancel
            </Button>
            <Button
              onClick={() => {
                onConfirm()
              }}
              autoFocus
              className="btn green-btn"
            >
              Ok
            </Button>
          </DialogActions>
        </Dialog>
  )
}

export default ConfirmationDialog;