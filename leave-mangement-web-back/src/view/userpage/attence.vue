<template>
    <el-card style="width:82%;" class="box-card">
        <div class="attence-box">
            <div>
                <comp-table :tableData="tableData" :tableHeader="tableHeader" :tableAttr="tableAttr" :headerCellStyle="headerCellStyle" @tableOtherClick="tableOtherClick" className="tableClassName"></comp-table>
                <el-pagination style="padding-top:10px" 
                @size-change="handleSizeChange" 
                :current-page="currentPage" 
                :page-size="pageSize" 
                layout="total, prev, pager, next" :total="totalCount">
                </el-pagination>
            </div>
            <div>
                <ul class="attence-tip">
                    <li><span style="background:#5fb878"></span> ：正常出勤</li>
                    <li><span style="background:red"></span> ：缺勤</li>
                    <li><span style="background:#50bfff"></span> ：今天</li>
                </ul>
                <div class="attence-calendar-box">
                    <calendar-comp ref='calendar' class="attence-calendar-item"></calendar-comp>
                </div>
            </div>
        </div>
    </el-card>
</template>
<script>
    import calendarComp from '@/packages/components/calendar.vue'
    import CompTable from '@/packages/components/table'
    import {
        UserPageApi
    } from "./api.js";
    export default {
        components: {
            calendarComp,
            CompTable
        },
        data() {
            return {
                currentPage: 1,
                pageSize: 15,
                totalCount: 0,

                tableAttr: {
                    noIndex: false,
                    other: [{
                            name: '查看',
                            style: 'text',
                            type: 'look',
                            icon: 'el-icon-view',
                            color: '#409eff'
                        }
                    ]
                },
                tableHeader: [{
                    prop: 'month',
                    label: '日期',
                    width: 180,
                }, {
                    prop: 'clockCount',
                    label: '出勤数（天）',
                    width: 120
                }, {
                    prop: 'leaveCount',
                    label: '请假数（次）',
                    width: 120
                }],
                tableData: [],
                headerCellStyle: {
                    backgroundColor: '#f2f2f2',
                    fontSize: '14px',
                    color: '#434343',
                    fontWeight: 400
                },
                value: new Date()
            }
        },
        mounted() {
            this.loadData()
        },
        methods: {
            loadData() {
                UserPageApi.getAttence().then(data => {
                    console.log(data.data)
                    this.tableData = data.data.data
                    this.totalCount = data.data.count
                })
            },
             handleSizeChange(val) {
                this.pageSize = val;
                this.loadData();
            },
            handleCurrentChange(val) {
                this.currentPage = val;
                this.loadData();
            },
            tableOtherClick(row, type, index) {
                if (type === 'look') {
                    this.$refs.calendar.transfromMonth(row.month+'-01')
                }
            },
        },
    }
</script>