import React, { useEffect, useMemo, useState } from "react";
import { productStyle } from "./style";
import { defaultFilter, RecordsPerPage } from "../../constant/constant";
import { useHistory } from "react-router-dom";
import {
  Typography,
  TextField,
} from "@material-ui/core";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TablePagination from "@material-ui/core/TablePagination";
import TableRow from "@material-ui/core/TableRow";
import { Button } from "@material-ui/core";
import userService from "../../service/user.service";
import { toast } from "react-toastify";
import Role from "../../models/RoleModel";
import BaseList from "../../models/BaseList";
import FilterModel from "../../models/FilterModel";
import UserModel from "../../models/UserModel";
import RoleModel from "../../models/RoleModel";
import { AuthContextModel, useAuthContext } from "../../context/auth";
import ConfirmationDialog from "../../components/ConfirmationDialog";
import Shared from "../../utils/shared";

const User: React.FC = () => {
  const classes = productStyle();
  const authContext: AuthContextModel = useAuthContext();
  const [filters, setFilters] = useState<FilterModel>(defaultFilter);
  const [userList, setUserList] = useState<BaseList<UserModel[]>>({
    records: [],
    totalRecords: 0,
  });
  const [open, setOpen] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);
  const [roles, setRoles] = useState<RoleModel[]>([]);

  const history = useHistory();
  useEffect(() => {
    getAllUserRoles();
  }, []);

  useEffect(() => {
    const timer: NodeJS.Timeout = setTimeout(() => {
      if(filters.keyword==="") delete filters.keyword
      getAllUsers({ ...filters });
    }, 500);
    return () => clearTimeout(timer);
  }, [filters]);

  const getAllUsers = async (filters: FilterModel): Promise<void> => {
    await userService.getAllUsers(filters).then((res) => {
      if (res) {
        setUserList(res);
      }
    });
  };

  const getAllUserRoles = async (): Promise<void> => {
    await userService.getAllRoles().then((res: BaseList<Role[]>) => {
      console.log("ahi......",res.records.length)
      if (res.records.length) {
        setRoles(res.records);
      }
    });
  };
     console.log("sss",userList.records);
  const users: UserModel[] = useMemo((): UserModel[] => {
    if (userList?.records) {
      console.log("abhi1",roles)
       userList?.records.forEach((element: UserModel) => {
         element.role = roles.find(
           (a) => a.id === element.roleId
         )?.name;
       });
       console.log("abhi2",userList.records)
      return userList.records;
    }
    return [];
  }, [roles, userList]);

  const columns = [
    { id: "firstName", label: "First Name", minWidth: 100 },
    { id: "lastName", label: "Last Name", minWidth: 100 },
    {
      id: "email",
      label: "Email",
      minWidth: 170,
    },
    {
      id: "roleName",
      label: "Role",
      minWidth: 130,
    },
  ];

  const onConfirmDelete = async (): Promise<void> => {
    await userService.delete(selectedId).then((res) => {
      if (res) {
        toast.success(Shared.messages.DELETE_SUCCESS);
        setOpen(false);
        setFilters({ ...filters });
      }
    });
  };

  return (
    <div className={classes.productWrapper}>
      <div className="container">
        <Typography variant="h1">User</Typography>
        <div className="btn-wrapper">
          <TextField
            id="text"
            name="text"
            placeholder="Search..."
            variant="outlined"
            inputProps={{ className: "small" }}
            onChange={(e) => {
              setFilters({ ...filters, keyword: e.target.value, pageIndex: 1 });
            }}
          />
        </div>
        <TableContainer>
          <Table aria-label="simple table">
            <TableHead>
              <TableRow>
                {columns.map((column) => (
                  <TableCell
                    key={column.id}
                    style={{ minWidth: column.minWidth }}
                  >
                    {column.label}
                  </TableCell>
                ))}
                <TableCell></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {users.map((row: UserModel, index: number) => (
                <TableRow key={row.id}>
                  <TableCell>{row.firstName}</TableCell>
                  <TableCell>{row.lastName}</TableCell>
                  <TableCell>{row.email}</TableCell>
                  <TableCell>{row.role}</TableCell>
                  <TableCell>
                    <Button
                      type="button"
                      className="green-btn btn"
                      variant="contained"
                      color="primary"
                      disableElevation
                      onClick={() => {
                        history.push(`/edit-user/${row.id}`);
                      }}
                    >
                      Edit
                    </Button>
                    {row.id !== authContext.user.id && (
                      <Button
                        type="button"
                        className="btn pink-btn"
                        variant="contained"
                        color="primary"
                        disableElevation
                        onClick={() => {
                          setOpen(true);
                          setSelectedId(row.id ?? 0);
                        }}
                      >
                        Delete
                      </Button>
                    )}
                  </TableCell>
                </TableRow>
              ))}
              {!users.length && (
                <TableRow className="TableRow">
                  <TableCell colSpan={5} className="TableCell">
                    <Typography align="center" className="noDataText">
                      No Users
                    </Typography>
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>

        <TablePagination
          rowsPerPageOptions={RecordsPerPage}
          component="div"
          count={userList?.records.length ? userList.totalRecords : 0}
          rowsPerPage={filters.pageSize || 0}
          page={filters.pageIndex - 1}
          onPageChange={(e, newPage) => {
            setFilters({ ...filters, pageIndex: newPage + 1 });
          }}
          onRowsPerPageChange={(e) => {
            setFilters({
              ...filters,
              pageIndex: 1,
              pageSize: Number(e.target.value),
            });
          }}
        />

        <ConfirmationDialog
          open={open}
          onClose={() => setOpen(false)}
          onConfirm={() => onConfirmDelete()}
          title="Delete user"
          description={Shared.messages.USER_DELETE}
        />
      </div>
    </div>
  );
};

export default User;
