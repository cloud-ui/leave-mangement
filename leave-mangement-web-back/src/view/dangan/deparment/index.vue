<template>
    <div class="comp">
        <div class="comp-title">
            <p>公司部门</p>
        </div>
        <div class="comp-body">
            <div class="comp-body-title"> 
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 个部门</p>
                    <el-button style="padding:0px 0px 0px 10px;" @click="dialogVisible = true" type="text" icon="el-icon-plus">添加部门</el-button>
                </div>
                    <el-input style="width:200px;" placeholder="请输入内容" :change="handleChangeQuery()" v-model="query">
                    <i slot="prefix" class="el-input__icon el-icon-search"></i></el-input>
            </div>
            <el-table :data="tableData" border stripe style="width: 100%">
             <el-table-column
                prop="code"
                label="编号"
                align="center"
                width="80">
             </el-table-column>
             <el-table-column
                prop="name"
                align="center"
                label="名称">
             </el-table-column>
             <el-table-column
                prop="manger"
                width="200"
                align="center"
                label="经理">
             </el-table-column>
             <el-table-column
                prop="workerCount"
                width="100"
                align="center"
                label="部门人数">
                <template slot-scope="scope">
                    <a>{{scope.row.workerCount}}</a>
                </template>
             </el-table-column>
             <el-table-column
                label="操作"
                header-align="center"
                width="100">
                <template slot-scope="scope">
                    <el-button @click="handleClick(scope.row)" type="text" size="small">查看</el-button>
                    <el-button type="text" size="small">编辑</el-button>
                </template>
             </el-table-column>
            </el-table>
            <el-pagination 
            style="padding-top:10px"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page="currentPage"
            :page-sizes="[100, 200, 300, 400]"
            :page-size="pageSize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalCount">
            </el-pagination>
        </div>
        <el-dialog :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-adddep @closeForm='handleClose' @close='closeForm' ref="compForm"></comp-adddep>
    </el-dialog>
    </div>
</template>
<script>
import '../dangan.scss'
import CompAdddep from './adddep'
import {FileApi} from '../api.js'
export default {
    data(){
        return{
            tableData:[],
            totalCount:0,
            query:'',
            currentPage:1,
            pageSize:20,
            dialogVisible:false
        }
    },
    components:{
        CompAdddep,
    },
    mounted(){
        this.loadData()
    },
    methods:{
        handleSizeChange(val) {
            this.pageSize = val;
            this.loadData();
      },
      handleCurrentChange(val) {
          this.currentPage = val;
          this.loadData();
      },
      handleChangeQuery(){
          this.loadData()
      },
      loadData(){
          const params = {
              currentPage:this.currentPage,
              currentPageSize:this.pageSize,
              query:this.query
          };
          FileApi.getDeparmentList(params).then(res=>{
              this.totalCount = res.data.totalCount;
              this.tableData = res.data.data;
          });
      },
      //关闭弹窗
      handleClose(){
          this.dialogVisible = false
      },
      closeForm(dialogVisible){
      this.dialogVisible = dialogVisible
    }
    }
}
</script>
<style>
.el-dialog__body {
    padding: 10px 20px;
    color: #606266;
    font-size: 14px;
}
</style>

