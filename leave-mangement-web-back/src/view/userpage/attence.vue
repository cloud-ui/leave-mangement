<template>
    <el-card style="width:82%;" class="box-card">
        <div class="attence-box">
            <div>
                <comp-table :tableData="tableData" :tableHeader="tableHeader" :tableAttr="tableAttr" :headerCellStyle="headerCellStyle" 
                className="tableClassName"></comp-table>
            </div>
            <div>
                <ul class="attence-tip">
                    <li><span style="background:#5fb878"></span> ：正常出勤</li>
                    <li><span style="background:red"></span> ：缺勤</li>
                    <li><span style="background:#50bfff"></span> ：今天</li>
                </ul>
                <div class="attence-calendar-box">
                    <calendar-comp class="attence-calendar-item"></calendar-comp>
                </div>
            </div>
        </div>
            
    </el-card>
</template>
<script>
import calendarComp from '@/packages/components/calendar.vue'
import CompTable from '@/packages/components/table'
export default {
    components: {
        calendarComp,
        CompTable
    },
    data(){
        return{
            tableAttr: {},
                tableHeader: [{
                    prop: 'yearMonth',
                    label: '日期',
                    width: 180,
                }, {
                    prop: 'workCount',
                    label: '出勤数（天）',
                    width:120
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
    }
}
</script>