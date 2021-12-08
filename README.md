<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/285862183/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T938922)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Read-Only and Enabled State Binding

The example uses [BaseColumn.IsEnabledBinding](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.BaseColumn.IsEnabledBinding) property to disable the **StartDate** and **Due Date** cells if the **Progress** value equals **100%**:

![](./grid-enabled-binding.png)

... and the [BaseColumn.IsReadOnlyBinding](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.BaseColumn.IsReadOnlyBinding) property to make the **Progress** cells read-only if a task has subtasks:

![](./grid-read-only-binding.png)

---

## TIP
In versions prior to **v20.2**, please use solutions from this example: [How to disable rows in GridControl based on their values](https://github.com/DevExpress-Examples/how-to-disable-rows-in-gridcontrol-based-on-their-values-e3594).
