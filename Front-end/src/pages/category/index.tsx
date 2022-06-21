import React, { useEffect, useState } from "react";
import { productStyle } from "./style";
import {
  defaultFilter,
  RecordsPerPage,
  StatusCode,
} from "../../constant/constant";
import { useHistory } from "react-router-dom";
import {
  Typography,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
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
import categoryService from "../../service/category.service";
import { toast } from "react-toastify";
import FilterModel from "../../models/FilterModel";
import BaseList from "../../models/BaseList";
import { CategoryModel } from "../../models/CategoryModel";
import ConfirmationDialog from "../../components/ConfirmationDialog";

const Category: React.FC = () => {
  const classes = productStyle();
  const [filters, setFilters] = useState<FilterModel>(defaultFilter);
  const [categoryRecords, setCategoryRecords] = useState<BaseList<CategoryModel[]>>({totalRecords: 0, records: []});
  const [open, setOpen] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);

  const history = useHistory();
  useEffect(() => {
    const timer: NodeJS.Timeout = setTimeout(() => {
      if(filters.keyword==="") delete filters.keyword
      searchAllCategories({ ...filters });
    }, 500);
    return () => clearTimeout(timer);
  }, [filters]);

  const searchAllCategories = (filters: FilterModel): void => {
    categoryService.getAll(filters).then((res) => {
      setCategoryRecords(res);
    });
  };

  const columns = [
    { id: "name", label: "Category Name", minWidth: 100 },
  ];

  const onConfirmDelete = (): void => {
    categoryService.delete(selectedId).then((res) => {
      toast.success("Record deleted successfully");
        setOpen(false);
        setFilters({ ...filters });
    });
  };
  return (
    <div className={classes.productWrapper}>
      <div className="container">
        <Typography variant="h1">Category</Typography>
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
          <Button
            type="button"
            className="btn pink-btn"
            variant="contained"
            color="primary"
            disableElevation
            onClick={() => history.push("/add-category")}
          >
            Add
          </Button>
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
              {categoryRecords?.records?.map((row: CategoryModel, index: number) => (
                <TableRow key={row.id}>
                  <TableCell>{row.name}</TableCell>
                  <TableCell>
                    <Button
                      type="button"
                      className="green-btn btn"
                      variant="contained"
                      color="primary"
                      disableElevation
                      onClick={() => {
                        history.push(`/edit-category/${row.id}`);
                      }}
                    >
                      Edit
                    </Button>
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
                  </TableCell>
                </TableRow>
              ))}
              {!categoryRecords?.records.length && (
                <TableRow className="TableRow">
                  <TableCell colSpan={6} className="TableCell">
                    <Typography align="center" className="noDataText">
                      No Category
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
          count={categoryRecords?.records.length ? categoryRecords.totalRecords : 0}
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
          title="Delete category"
          description="Are you sure you want to delete this category?"
        />
      </div>
    </div>
  );
};

export default Category;
