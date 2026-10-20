export const isDateRangeOverlapping = (
  userStart: string,
  userEnd: string,
  bookedStart: string,
  bookedEnd: string
): boolean => {
  const s1 = new Date(userStart).getTime();
  const e1 = new Date(userEnd).getTime();
  const s2 = new Date(bookedStart).getTime();
  const e2 = new Date(bookedEnd).getTime();

  return s1 < e2 && s2 < e1;
};